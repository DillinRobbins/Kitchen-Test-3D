using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : Ingredient
{
    private void Awake()
    {
        ingredientData = new CheeseType();
    }
}
