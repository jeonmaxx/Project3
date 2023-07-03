using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingManager : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public Recipe chosenRecipe;
    public Item itemOne;
    public Item itemTwo;

    private Item neededOne;
    private Item neededTwo;

    public bool allThere = false;

    public GameObject itemSlot;
    public InventoryItem[] inventoryItems;

    public void Update()
    {
        CheckRecipe();
        CheckIngredients();
    }

    public void CheckRecipe()
    {
        if(chosenRecipe != null)
        {
            neededOne = chosenRecipe.Product1;
            neededTwo = chosenRecipe.Product2;
        }
    }

    public void CheckIngredients()
    {
        if(chosenRecipe != null && itemOne != null && itemTwo != null)
        {
            if((itemOne == neededOne && itemTwo == neededTwo) || (itemOne == neededTwo && itemTwo == neededOne))
            {
                allThere = true;
                inventoryItems = itemSlot.GetComponentsInChildren<InventoryItem>();
            }
            else
            {
                allThere = false;
            }
        }
        else
            allThere = false;
    }

    public void BrewButton()
    {
        //Durch den Knopfdruck müsste QTE getriggert werden
        //InventoryItem[] inventoryItems;

        if (allThere)
        {
            inventoryManager.AddItem(chosenRecipe.Drink);
            for (int i = 0; i < inventoryItems.Length; i++)
            {
                inventoryItems[i].count--;

                if (inventoryItems[i].count <= 0)
                {
                    Destroy(inventoryItems[i].gameObject);
                }
                else
                {
                    inventoryItems[i].RefreshCount();
                }
            }
        }
    }
}
