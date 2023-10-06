using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedMeat : Ingredient
{
    private void Awake()
    {
        ingredientData = new CookedMeatType();
    }
}
