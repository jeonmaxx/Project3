using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BrewingManager : MonoBehaviour
{
    public InventoryManager inventoryManager;

    [HideInInspector] public Recipe chosenRecipe;
    /*[HideInInspector]*/ public Item itemOne;
    /*[HideInInspector]*/ public Item itemTwo;

    public Item neededOne;
    public Item neededTwo;

    public TMP_Text ingreOneText;
    public TMP_Text ingreTwoText;

    public bool allThere = false;
    private bool enoughOne = false;
    private bool enoughTwo = false;

    public GameObject itemOneObj;
    public GameObject itemTwoObj;
    public InventoryItem inventoryItemOne;
    public InventoryItem inventoryItemTwo;

    private OpenBrewing open;
    private bool brewing = false;

    public int quantity;

    private void Start()
    {
        open = GetComponent<OpenBrewing>();
    }

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
            ingreOneText.SetText(chosenRecipe.Product1.itemName);
            neededTwo = chosenRecipe.Product2;
            ingreTwoText.SetText(chosenRecipe.Product2.itemName);
        }
    }

    public void CheckIngredients()
    {
        if (chosenRecipe != null)
        {
            if (itemOne == neededOne)
            {
                inventoryItemOne = itemOneObj.GetComponentInChildren<InventoryItem>();
            }
            if(itemTwo == neededTwo)
            {
                inventoryItemTwo = itemTwoObj.GetComponentInChildren<InventoryItem>();
            }

            int amountOne = chosenRecipe.Product1Amount * quantity;
            int amountTwo = chosenRecipe.Product2Amount * quantity;

            if (inventoryItemOne != null)
            {
                if (inventoryItemOne.count >= amountOne)
                    enoughOne = true;
                else
                    enoughOne = false;
            }

            if (inventoryItemTwo != null)
            {
                if (inventoryItemTwo.count >= amountTwo)
                    enoughTwo = true;
                else
                    enoughTwo = false;
            }
        }

    }

    public void ConfirmRecipeButton(int brew)
    {
        if(brew == 0 && chosenRecipe != null && quantity != 0 && !brewing)
        {
            open.Close(false);
            open.currentRect = open.menus[1];
            open.Open(false);
        }

        else if(brew == 1 && itemOne == neededOne && enoughOne)
        {
            open.Close(false);
            open.currentRect = open.menus[2];
            open.Open(false);
        }

        else if(brew == 2 && itemTwo == neededTwo && enoughTwo)
        {
            open.Close(false);
            open.currentRect = open.menus[0];
            open.Open(false);


            //hier eigentlich Brauzeit und dann wird gedroppt
            //brewing = true;
            BrewButton();
        }
    }

    public void BrewButton()
    {
        //Durch den Knopfdruck müsste QTE getriggert werden

        if (enoughOne && enoughTwo)
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

                //Items die noch drin sind werden danach zurück gegeben
                if (inventoryItemOne.count != 0)
                {
                    int temp = inventoryItemOne.count;
                    for (int i = 0; i < temp; i++)
                    {
                        inventoryManager.AddItem(itemOne);
                        inventoryItemOne.count--;
                    }
                    RefreshItems(inventoryItemOne);
                }

                if (inventoryItemTwo.count != 0)
                {
                    int temp = inventoryItemTwo.count;
                    for (int i = 0; i < temp; i++)
                    {
                        inventoryManager.AddItem(itemTwo);
                        inventoryItemTwo.count--;
                    }
                    RefreshItems(inventoryItemTwo);
                }
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
