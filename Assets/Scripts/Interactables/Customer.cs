using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Customer : MonoBehaviour
{
    private float ingredientCount;
    private List<IngredientDataType> orderList = new();
    private List<GameObject> orderDisplayGameObjectList = new();
    private TextMeshProUGUI orderTimer;

    private GameObject orderImagePrefab;

    private float timer;
    private float scoreToAdd;
    public UnityAction<int> OnOrderFulfilled;

    private void Awake()
    {
        ingredientCount = RollIngredientCount();
        

        for(int i = 0; i < ingredientCount; i++)
        {
            var ingredient = ReturnRandomIngredient();
            orderList.Add(ingredient);
            scoreToAdd += ingredient.GetPoints();
        }
    }

    private void Start()
    {
        orderImagePrefab = Resources.Load<GameObject>("Prefab/Order Image");

        foreach (var ingredient in orderList)
        {
            var orderObj = Instantiate(orderImagePrefab, transform);
            orderObj.GetComponent<Image>().sprite = ingredient.GetSprite();
            orderDisplayGameObjectList.Add(orderObj);
        }

        orderTimer = GetComponentInChildren<TextMeshProUGUI>();

        timer = scoreToAdd;
        orderTimer.text = scoreToAdd.ToString();
    }

    private void FixedUpdate()
    {
        scoreToAdd -= Time.deltaTime;

        orderTimer.text = Mathf.FloorToInt(scoreToAdd).ToString();
    }

    private int RollIngredientCount()
    {
        int roll = UnityEngine.Random.Range(1, 101);

        if (roll <= 50) return 2;
        return 3;
    }

    private IngredientDataType ReturnRandomIngredient()
    {
        int roll = UnityEngine.Random.Range(0, 3);
        if (roll == 0) return new CheeseType();
        else if(roll == 1) return new SaladType();
        else return new CookedMeatType();
    }

    public IngredientDataType IngredientMatchesOrder(Ingredient ingredient)
    {
        if (ingredient == null) return null;

        foreach(var order in orderList)
        {
            if (order.GetType() == ingredient.GetIngredientDataType().GetType()) return order;
        }
        return null;
    }

    public void GiveIngredient(ICanHoldIngredient ingredientInventory, Ingredient ingredient)
    {
        var orderToRemove = IngredientMatchesOrder(ingredient);

        if (orderToRemove != null)
        {
            ingredientInventory.TakeIngredient(null);
            var orderDisplayToRemove = orderDisplayGameObjectList.Find(x => x.GetComponent<Image>().sprite == ingredient.GetIngredientDataType().GetSprite());
            orderDisplayGameObjectList.Remove(orderDisplayToRemove);
            Destroy(orderDisplayToRemove);

            orderList.Remove(orderToRemove);
        }
        else
            Debug.Log("You don't have the right ingredient");

        if (orderList.Count == 0) CompleteCustomerOrder();
    }

    public int GetScoreToAdd()
    {
        return Mathf.FloorToInt(scoreToAdd);
    }

    private void CompleteCustomerOrder()
    {
        OnOrderFulfilled.Invoke(Mathf.FloorToInt(scoreToAdd));
        Destroy(gameObject);
    }
}
