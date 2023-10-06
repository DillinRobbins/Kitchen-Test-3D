using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : Ingredient
{
    private void Awake()
    {
        ingredientData = new VegetableType();
    }
}
