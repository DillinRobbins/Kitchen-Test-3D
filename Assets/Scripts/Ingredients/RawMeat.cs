using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMeat : Ingredient
{
    private void Awake()
    {
        ingredientData = new RawMeatType();
    }
}
