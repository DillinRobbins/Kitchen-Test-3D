using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salad : Ingredient
{
    private void Awake()
    {
        ingredientData = new SaladType();
    }
}
