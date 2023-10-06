using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour, ICanHoldIngredient
{
    [SerializeField] private Transform ingredientContainer;
    
    private Ingredient heldIngredient;

    public static UnityAction<ICanHoldIngredient> OnInventoryInitialize;

    private void Start()
    {
        var inventoryInterface = GetComponent<ICanHoldIngredient>();
        OnInventoryInitialize.Invoke(inventoryInterface);
    }

    public bool HasIngredient()
    {
        if(heldIngredient == null) return false;
        return true;
    }

    public Ingredient CheckIngredient()
    {
        return heldIngredient;
    }

    private void PlaceIngredientOnPlate(Ingredient ingredient)
    {
        ingredient.transform.SetParent(ingredientContainer, false);
    }

    private void PlaceIngredientInNewContainer(Transform newIngredientContainer)
    {
        heldIngredient.transform.SetParent(newIngredientContainer, false);
        heldIngredient = null;
    }

    public void GiveIngredient(Ingredient ingredient)
    {
        if (ingredient == null) Debug.Log("Received a null ingredient");
        if(heldIngredient != null) Destroy(heldIngredient.gameObject);
        heldIngredient = ingredient;
        PlaceIngredientOnPlate(ingredient);
    }

    public Ingredient TakeIngredient(Transform newContainer)
    {
        if (newContainer == null) Destroy(heldIngredient.gameObject);

        if(heldIngredient != null)
        {
            PlaceIngredientInNewContainer(newContainer);
        }

        Debug.Log("You aren't carrying an ingredient");
        return null;
    }
}
