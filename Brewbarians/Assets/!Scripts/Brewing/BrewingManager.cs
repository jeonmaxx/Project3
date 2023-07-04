using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingManager : MonoBehaviour
{
    public InventoryManager inventoryManager;

    [HideInInspector] public Recipe chosenRecipe;
    [HideInInspector] public Item itemOne;
    [HideInInspector] public Item itemTwo;

    private Item neededOne;
    private Item neededTwo;

    public bool allThere = false;
    private bool enough = false;

    public GameObject itemOneObj;
    public GameObject itemTwoObj;
    private InventoryItem inventoryItemOne;
    private InventoryItem inventoryItemTwo;

    public int quantity;

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
        if (chosenRecipe != null && itemOne != null && itemTwo != null)
        {
            if (itemOne == neededOne && itemTwo == neededTwo)
            {
                allThere = true;
                inventoryItemOne = itemOneObj.GetComponentInChildren<InventoryItem>();
                inventoryItemTwo = itemTwoObj.GetComponentInChildren<InventoryItem>();
            }
            else if (itemOne == neededTwo && itemTwo == neededOne)
            {
                allThere = true;
                inventoryItemTwo = itemOneObj.GetComponentInChildren<InventoryItem>();
                inventoryItemOne = itemTwoObj.GetComponentInChildren<InventoryItem>();
            }
            else
            {
                allThere = false;
            }

            int amountOne = chosenRecipe.Product1Amount * quantity;
            int amountTwo = chosenRecipe.Product2Amount * quantity;

            if (inventoryItemOne != null && inventoryItemTwo != null)
            {
                if (inventoryItemOne.count >= amountOne && inventoryItemTwo.count >= amountTwo)
                    enough = true;
                else
                    enough = false;
            }
        }
        else
            allThere = false;

    }

    public void BrewButton()
    {
        //Durch den Knopfdruck müsste QTE getriggert werden

        if (allThere && enough)
        {
            for (int i = 0; i < quantity; i++)
            {
                inventoryManager.AddItem(chosenRecipe.Drink);
            }

            if (inventoryItemOne.count > 0 && inventoryItemTwo.count > 0)
            {
                inventoryItemOne.count -= (chosenRecipe.Product1Amount * quantity);
                inventoryItemTwo.count -= (chosenRecipe.Product2Amount * quantity);

                RefreshItems(inventoryItemOne);
                RefreshItems(inventoryItemTwo);
            }
        }
    }

    private void RefreshItems(InventoryItem item)
    {
        if (item.count <= 0)
        {
            Destroy(item.gameObject);
        }
        else
        {
            item.RefreshCount();
        }
    }
}
