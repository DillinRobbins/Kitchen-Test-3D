using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanHoldIngredient
{
    public bool HasIngredient();

    public Ingredient CheckIngredient();

    public void GiveIngredient(Ingredient ingredient);

    public Ingredient TakeIngredient(Transform newIngredientContainer);
}
