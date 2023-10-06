using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Stove : MonoBehaviour, IInteractable
{
    private Color32 off = new(231, 36, 106, 255);
    private Color32 on = new(124, 238, 100, 255);

    [SerializeField] private SpriteRenderer ring;

    private ICanHoldIngredient ingredientInventory;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private Slider progressBarSlider;

    private Ingredient heldIngredient;
    [SerializeField] private Transform ingredientContainer;
    [SerializeField] private GameObject cookedMeat;

    bool foodPrepared = false;

    private float timer = 6f;
    private float maxTime = 6f;

    private void OnEnable()
    {
        PlayerInventory.OnInventoryInitialize += InitializeInventoryInterface;
    }

    private void OnDisable()
    {
        PlayerInventory.OnInventoryInitialize -= InitializeInventoryInterface;
    }

    public void Interact(Ingredient ingredient)
    {
        if (foodPrepared)
        {
            ingredientInventory.GiveIngredient(heldIngredient);
            heldIngredient = null;
            foodPrepared = false;
        }
        else if (ingredient is RawMeat)
        {
            ingredientInventory.TakeIngredient(ingredientContainer);
            heldIngredient = ingredient;
            StartCoroutine(PrepareFood());
        }
        else Debug.Log("No viable ingredient for stove");
    }

    private void OnTriggerEnter(Collider collision)
    {
        ring.color = on;
    }

    private void OnTriggerExit(Collider collision)
    {
        ring.color = off;
    }

    private IEnumerator PrepareFood()
    {
        progressBar.SetActive(true);

        while(timer > 0)
        {
            timer -= Time.deltaTime;
            progressBarSlider.value = (maxTime - timer) / maxTime;
            yield return null;
        }
        ResetProgressBar();
        timer = maxTime;

        foodPrepared = true;
        TurnRawMeatIntoCookedMeat();
    }

    private void ResetProgressBar()
    {
        progressBar.SetActive(false);
        progressBarSlider.value = 0;
    }

    private void TurnRawMeatIntoCookedMeat()
    {
        Destroy(heldIngredient.gameObject);
        var ingredientObj = Instantiate(cookedMeat, ingredientContainer);
        heldIngredient = ingredientObj.GetComponent<CookedMeat>();
    }

    private void InitializeInventoryInterface(ICanHoldIngredient inventoryInterface)
    {
        ingredientInventory = inventoryInterface;
    }
}
