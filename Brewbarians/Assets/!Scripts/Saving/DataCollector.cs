using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class MainItems
{
    public Item MainItem;
    public int Counter;
    public MainItems(Item mainItem, int count)
    {
        MainItem = mainItem;
        Counter = count;
    }
}
public class DataCollector : MonoBehaviour
{
    [Header("Input")]
    public PlayerMovement playerMovement;
    public InventoryManager inventoryManager;
    public RecipeManager recipeManager;

    [Header("Data")]
    public Vector3 playerPosition;
    public List<MainItems> mainItems;
    public List<InventoryItem> mainSeeds;
    public List<RecipeItem> recipes;

    private InventoryItem tmpInven;

    public void Update()
    {
        //CollectData();
    }

    public void CollectData()
    {
        //Player Position
        playerPosition = playerMovement.gameObject.transform.position;

        //Main Items
        mainItems = new List<MainItems>();
        for (int i = 0; i < inventoryManager.inventorySlots.Length; i++)
        {
            if (inventoryManager.inventorySlots[i].transform.childCount != 0)
            {
                tmpInven = inventoryManager.inventorySlots[i].transform.GetChild(0).GetComponent<InventoryItem>();
                mainItems.Add(new MainItems(tmpInven.item, tmpInven.count));
            }
            else
            {
                mainItems.Add(null);
            }
        }

        //Seeds
        mainSeeds = new List<InventoryItem>();
        for (int i = 0; i < inventoryManager.seedWheel.Length; i++)
        {
            if (inventoryManager.seedWheel[i].transform.childCount != 0)
            {
                mainSeeds.Add(inventoryManager.seedWheel[i].transform.GetChild(0).GetComponent<InventoryItem>());
            }
            else
            {
                mainSeeds.Add(null);
            }
        }

        //Recipes
        recipes = new List<RecipeItem>();
        if (recipeManager.recipeHolder.transform.childCount != 0)
        {
            for (int i = 0; i < recipeManager.recipeHolder.transform.childCount; i++)
            {
                recipes.Add(recipeManager.recipeHolder.transform.GetChild(i).GetComponent<RecipeItem>());
            }
        }
    }


    public void GiveData()
    {
        //Changes Player Position
        playerMovement.gameObject.transform.position = playerPosition;

        DeleteItems();
        GiveItems();
        //GiveRecipes();

        //CollectData();
    }

    public void DeleteItems()
    {
        //Delete all Items and give new ones
        for (int i = 0; i < inventoryManager.inventorySlots.Length; i++)
        {
            if (inventoryManager.inventorySlots[i].transform.childCount != 0)
            {
                Destroy(inventoryManager.inventorySlots[i].transform.GetChild(0).gameObject);
            }
        }
    }

    public void GiveItems()
    {
        for (int i = 0; i < mainItems.Count; i++)
        {
            //inventoryManager.AddItemInSlot(mainItems[i].MainItem, inventoryManager.inventorySlots[i]);
            for (int j = 0; j < mainItems[i].Counter && mainItems[i] != null; j++)
            {
                inventoryManager.AddItemInSlot(mainItems[i].MainItem, inventoryManager.inventorySlots[i]);
                //if (j == 0)
                //{
                //    inventoryManager.AddItemInSlot(mainItems[i].MainItem, inventoryManager.inventorySlots[i]);
                //}
                //else
                //{
                //    inventoryManager.inventorySlots[i].transform.GetComponentInChildren<InventoryItem>().count++;
                //}
            }
        }
    }

    //public void GiveRecipes()
    //{
    //    //Man bekommt mehr Recipes wenn man immer wieder loadet. Why ????

    //    //Deletes all Recipes
    //    for (int i = 0; i < recipeManager.recipeHolder.transform.childCount && recipeManager.recipeHolder.transform.childCount != 0; i++)
    //    {
    //        Destroy(recipeManager.recipeHolder.transform.GetChild(i).gameObject);
    //    }
    //    //Gives saved Recipes
    //    for (int j = 0; j < recipes.Count; j++)
    //    {
    //        recipeManager.AddRecipe(recipes[j].recipe);
    //    }
    //}
}
