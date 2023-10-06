using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour, IInteractable
{
    private Color32 off = new(231, 36, 106, 255);
    private Color32 on = new(124, 238, 100, 255);

    [SerializeField] private SpriteRenderer ring;

    private bool foodPrepared = false;

    private float timer = 2f;
    private float maxTime = 2f;

    private ICanHoldIngredient ingredientInventory;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private Slider progressBarSlider;

    private Ingredient heldIngredient;
    [SerializeField] private Transform ingredientContainer;
    [SerializeField] private GameObject salad;

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
        else if (heldIngredient != null) StartCoroutine(PrepareFood());
        else if (ingredient is Vegetable)
        {
            ingredientInventory.TakeIngredient(ingredientContainer);
            heldIngredient = ingredient;
            StartCoroutine(PrepareFood());
        }
        else Debug.Log("No viable ingredient for table");
    }

    private void OnTriggerEnter(Collider collision)
    {
        ring.color = on;
    }

    private void OnTriggerExit(Collider collision)
    {
        ring.color = off;
       StopAllCoroutines();
        //ResetProgressBar();
    }

    private IEnumerator PrepareFood()
    {
        progressBar.SetActive(true);

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            progressBarSlider.value = (maxTime - timer) / maxTime;
            yield return null;
        }
        timer = maxTime;
        ResetProgressBar();

        foodPrepared = true;
        TurnLettuceIntoSalad();
    }

    private void ResetProgressBar()
    {
        progressBar.SetActive(false);
        progressBarSlider.value = 0;
    }

    private void TurnLettuceIntoSalad()
    {
        Destroy(heldIngredient.gameObject);
        var ingredientObj = Instantiate(salad, ingredientContainer);
        heldIngredient = ingredientObj.GetComponent<Salad>();
    }

    private void InitializeInventoryInterface(ICanHoldIngredient inventoryInterface)
    {
        ingredientInventory = inventoryInterface;
    }
}
