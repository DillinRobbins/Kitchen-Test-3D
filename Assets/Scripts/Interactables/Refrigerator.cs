using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour, IInteractable
{
    private Color32 off = new(231, 36, 106, 255);
    private Color32 on = new(124, 238, 100, 255);

    [SerializeField] private SpriteRenderer ring;
    [SerializeField] private GameObject ingredientButtons;

    [SerializeField] private GameObject rawMeat;
    [SerializeField] private GameObject vegetable;
    [SerializeField] private GameObject cheese;

    ICanHoldIngredient ingredientInventory;

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
        if(ingredientButtons.activeSelf) ingredientButtons.SetActive(false);
        else ingredientButtons.SetActive(true);
    }

    public void GiveNewIngredient(int ingredient)
   {
        Ingredient ingredientToGive;
        GameObject ingredientObj;

        if (ingredient == 1) ingredientObj = Instantiate(cheese, transform);
        else if (ingredient == 2) ingredientObj = Instantiate(vegetable, transform);
        else ingredientObj = Instantiate(rawMeat, transform);

        ingredientToGive = ingredientObj.GetComponent<Ingredient>();
        ingredientInventory.GiveIngredient(ingredientToGive);
    }

    private void OnTriggerEnter(Collider collision)
    {
        ring.color = on;
    }

    private void OnTriggerExit(Collider collision)
    {
        ring.color = off;

        if(ingredientButtons.activeSelf) ingredientButtons.SetActive(false);
    }

    private void InitializeInventoryInterface(ICanHoldIngredient inventoryInterface)
    {
        ingredientInventory = inventoryInterface;
    }
}
