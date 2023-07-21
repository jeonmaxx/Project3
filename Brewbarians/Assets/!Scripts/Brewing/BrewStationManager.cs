using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GivenData
{
    public BrewingManager Brewing;
    public BrewingWait Wait;
    public GameObject IngreSlotOne;
    public GameObject IngreSlotTwo;

    public GivenData(BrewingManager brewing, BrewingWait wait, GameObject ingreOne, GameObject ingreTwo)
    {
        Brewing = brewing;
        Wait = wait;
        IngreSlotOne = ingreOne;
        IngreSlotTwo = ingreTwo;
    }
}

[Serializable]
public class BrewData
{
    public Recipe ChosenRecipe;
    public Item ItemOne;
    public int ItemOneCount;
    public Item ItemTwo;
    public int ItemTwoCount;
    public int Quantity;
    public int BonusPoints;
    public bool IsBrewing;
    public float ProgressBarValue;
    public BrewingStates BrewingStates;

    public BrewData(Recipe chosenRecipe, Item itemOne, int itemOneCount, Item itemTwo, int itemTwoCount, int quantity, int bonusPoints, bool isBrewing, float progressBar, BrewingStates brewingStates)
    {
        ChosenRecipe = chosenRecipe;
        ItemOne = itemOne;
        ItemOneCount = itemOneCount;
        ItemTwo = itemTwo;
        ItemTwoCount = itemTwoCount;
        Quantity = quantity;
        BonusPoints = bonusPoints;
        IsBrewing = isBrewing;
        ProgressBarValue = progressBar;
        BrewingStates = brewingStates;
    }
}

public class BrewStationManager : MonoBehaviour
{
    public List<GivenData> givenData = new List<GivenData>();
    public List<BrewData> brewData = new List<BrewData>();
    public InventoryManager invenManager;

    private InventoryItem tmpInven;
    private int tmpCount;

    public void AddData()
    {
        brewData.Clear();
        for(int i = 0; i < givenData.Count; i++)
        {
            BrewingManager brew = givenData[i].Brewing;
            BrewingWait wait = givenData[i].Wait;

            Recipe recipe = brew.chosenRecipe != null ? brew.chosenRecipe : null;

            Item one = null;
            int oneInt = 0;
            if(brew.itemOne != null)
            {
                InventoryItem invenOne = givenData[i].IngreSlotOne.transform.GetChild(0).GetComponent<InventoryItem>();
                one = invenOne.item;
                oneInt = invenOne.count;
            }

            Item two = null;
            int twoInt = 0;
            if (brew.itemTwo != null)
            {
                InventoryItem invenTwo = givenData[i].IngreSlotTwo.transform.GetChild(0).GetComponent<InventoryItem>();
                two = invenTwo.item;
                twoInt = invenTwo.count;
            }

            BrewingStates state = givenData[i].Brewing.gameObject.transform.GetComponent<OpenBrewing>().state;

            brewData.Add( new BrewData(recipe, one, oneInt, two, twoInt, brew.quantity, brew.bonusPoints,
                brew.brewing, wait.progressBar.value, state));
        }
    }

    //Bug, dass die Ingredients nicht ganz gel�scht werden
    public void LoadData()
    {
        for (int i = 0; i < brewData.Count; i++)
        {
            BrewingManager brew = givenData[i].Brewing;
            BrewingWait wait = givenData[i].Wait;

            //Recipe
            brew.chosenRecipe = brewData[i].ChosenRecipe;


            //Ingredients
            if (brewData[i].ItemOne != null)
            {
                ChosenIngre chosen = givenData[i].IngreSlotOne.transform.GetComponent<ChosenIngre>();
                chosen.chosenItem = brewData[i].ItemOne;

                if (givenData[i].IngreSlotOne.transform.childCount > 0)
                {
                    Destroy(givenData[i].IngreSlotOne.transform.GetChild(0).gameObject);
                }

                InventoryItem inventoryItem = null;
                tmpCount = brewData[i].ItemOneCount;
                for (int j = 0; j < tmpCount; j++)
                {
                    if (j == 0)
                    {
                        GameObject newItemGo = Instantiate(invenManager.inventoryItemPrefab, givenData[i].IngreSlotOne.transform);
                        inventoryItem = newItemGo.GetComponent<InventoryItem>();
                        inventoryItem.InitialiseItem(brewData[i].ItemOne);
                    }
                    else if (j > 0)
                    {
                        inventoryItem.count++;
                        inventoryItem.RefreshCount();
                    }
                }
            }

            if (brewData[i].ItemTwo != null)
            {
                ChosenIngre chosen = givenData[i].IngreSlotTwo.transform.GetComponent<ChosenIngre>();
                chosen.chosenItem = brewData[i].ItemTwo;

                if (givenData[i].IngreSlotTwo.transform.childCount > 0)
                {
                    Destroy(givenData[i].IngreSlotTwo.transform.GetChild(0).gameObject);
                }

                InventoryItem inventoryItem = null;
                tmpCount = brewData[i].ItemTwoCount;
                for (int j = 0; j < tmpCount; j++)
                {
                    if (j == 0)
                    {
                        GameObject newItemGo = Instantiate(invenManager.inventoryItemPrefab, givenData[i].IngreSlotTwo.transform);
                        inventoryItem = newItemGo.GetComponent<InventoryItem>();
                        inventoryItem.InitialiseItem(brewData[i].ItemTwo);
                    }
                    else if (j > 0)
                    {
                        inventoryItem.count++;
                        inventoryItem.RefreshCount();
                    }
                }
            }

            //Rest
            brew.quantity = brewData[i].Quantity;
            brew.bonusPoints = brewData[i].BonusPoints;
            brew.brewing = brewData[i].IsBrewing;
            wait.progressBar.value = brewData[i].ProgressBarValue;
            brew.gameObject.transform.GetComponent<OpenBrewing>().state = brewData[i].BrewingStates;
        }
    }
}
