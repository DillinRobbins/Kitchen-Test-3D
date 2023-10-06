using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Ingredient : MonoBehaviour
{
    protected IngredientDataType ingredientData;
    public IngredientDataType GetIngredientDataType() => ingredientData;
}

public abstract class IngredientDataType
{
    protected virtual string Name { get; }
    protected virtual Sprite IngredientSprite { get; }
    protected virtual int Points { get; }

    public string GetName() => Name;

    public Sprite GetSprite() => IngredientSprite;

    public int GetPoints() => Points;
}

public  class CheeseType : IngredientDataType
{
    protected override string Name
    {
        get
        {
            return "Cheese";
        }
    }

    protected override Sprite IngredientSprite
    {
        get
        {
            return Resources.Load<Sprite>("Sprites/Cheese");
        }
    }

    protected override int Points
    {
        get
        {
            return 10;
        }
    }
}

public  class SaladType : IngredientDataType
{
    protected override string Name
    {
        get
        {
            return "Salad";
        }
    }

    protected override Sprite IngredientSprite
    {
        get
        {
            return Resources.Load<Sprite>("Sprites/Salad");
        }
    }

    protected override int Points
    {
        get
        {
            return 20;
        }
    }
}

public  class CookedMeatType : IngredientDataType
{
    protected override string Name
    {
        get
        {
            return "Cooked Meat";
        }
    }

    protected override Sprite IngredientSprite
    {
        get
        {
            return Resources.Load<Sprite>("Sprites/CookedMeat");
        }
    }

    protected override int Points
    {
        get
        {
            return 30;
        }
    }
}

public  class RawMeatType : IngredientDataType
{
    protected override string Name
    {
        get
        {
            return "Raw Meat";
        }
    }

    protected override Sprite IngredientSprite
    {
        get
        {
            return Resources.Load<Sprite>("Sprites/RawMeat");
        }
    }

    protected override int Points
    {
        get
        {
            return 0;
        }
    }
}

public  class VegetableType : IngredientDataType
{
    protected override string Name
    {
        get
        {
            return "Lettuce";
        }
    }

    protected override Sprite IngredientSprite
    {
        get
        {
            return Resources.Load<Sprite>("Sprites/Lettuce");
        }
    }

    protected override int Points
    {
        get
        {
            return 0;
        }
    }
}
