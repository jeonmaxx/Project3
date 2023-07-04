using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmountDisplay : MonoBehaviour
{
    public BrewingManager brewManager;
    public GameObject ingredientObj;
    private ChosenIngre ingredient;
    private InventoryItem inventoryItem;
    private TextMeshProUGUI amount;
    public int neededAmount;

    private void Start()
    {
        amount = GetComponent<TextMeshProUGUI>();
        ingredient = ingredientObj.GetComponent<ChosenIngre>();
    }

    private void Update()
    {
        CheckIngre();
    }

    public void CheckIngre()
    {
        if (brewManager.allThere)
        {
            if (ingredient.chosenItem == brewManager.chosenRecipe.Product1)
            {
                neededAmount = (brewManager.chosenRecipe.Product1Amount * brewManager.quantity);
                amount.SetText(neededAmount.ToString());
                CheckQuantity(neededAmount);
            }
            else if (ingredient.chosenItem == brewManager.chosenRecipe.Product2)
            {
                neededAmount = (brewManager.chosenRecipe.Product2Amount * brewManager.quantity);
                amount.SetText(neededAmount.ToString());
                CheckQuantity(neededAmount);
            }
        }
        else
            amount.SetText("");
    }

    public void CheckQuantity(int productAmount)
    {
        if(ingredientObj.transform.childCount != 0)
        {
            inventoryItem = ingredientObj.GetComponentInChildren<InventoryItem>();

            if (inventoryItem.count >= productAmount)
                amount.color = Color.black;
            else
                amount.color = Color.red;
        }
    }
}
