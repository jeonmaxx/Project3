using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    [Header("Input")]
    public PlayerMovement playerMovement;
    public InventoryManager inventoryManager;
    public RecipeManager recipeManager;

    [Header("Data")]
    public Vector3 playerPosition;
    public List<InventoryItem> mainItemSlots;
    public List<InventoryItem> mainSeedSlots;
    public List<RecipeItem> recipes;


    public void Update()
    {
        //CollectData();
    }

    public void CollectData()
    {
        //Player Position
        playerPosition = playerMovement.gameObject.transform.position;

        //Main Items
        mainItemSlots = new List<InventoryItem>();
        for (int i = 0; i < inventoryManager.inventorySlots.Length; i++)
        {
            if (inventoryManager.inventorySlots[i].transform.childCount != 0)
            {
                mainItemSlots.Add(inventoryManager.inventorySlots[i].transform.GetChild(0).GetComponent<InventoryItem>());
            }
            else
            {
                mainItemSlots.Add(null);
            }
        }

        //Seeds
        mainSeedSlots = new List<InventoryItem>();
        for (int i = 0; i < inventoryManager.seedWheel.Length; i++)
        {
            if (inventoryManager.seedWheel[i].transform.childCount != 0)
            {
                mainSeedSlots.Add(inventoryManager.seedWheel[i].transform.GetChild(0).GetComponent<InventoryItem>());
            }
            else
            {
                mainSeedSlots.Add(null);
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

        //Delete all Items and give new ones
        for (int i = 0; i < inventoryManager.inventorySlots.Length; i++)
        {
            if (inventoryManager.inventorySlots[i].transform.childCount != 0)
            {
                Destroy(inventoryManager.inventorySlots[i].transform.GetChild(0).gameObject);
            }
        }
        for(int i = 0; i < mainItemSlots.Count; i++)
        {
            if (mainItemSlots[i] != null)
            {
                int tmpCount = mainItemSlots[i].count;
                inventoryManager.SpawnNewItem(mainItemSlots[i].item, inventoryManager.inventorySlots[i]);
                //Count klappt nicht. Vielleicht mainItemSlots Liste in zwei Komponenten unterteilen? Inventory Items und Count?
                inventoryManager.inventorySlots[i].transform.GetChild(0).GetComponent<InventoryItem>().count = tmpCount;
            }
        }
        //Gives saved Recipes
        for (int j = 0; j < recipes.Count; j++)
        {
            recipeManager.AddRecipe(recipes[j].recipe);
        }


        //Deletes all Recipes
        for (int i = 0; i < recipeManager.recipeHolder.transform.childCount && recipeManager.recipeHolder.transform.childCount != 0; i++)
        {
            Destroy(recipeManager.recipeHolder.transform.GetChild(i).gameObject);
        }
        //Gives saved Recipes
        for (int j = 0; j < recipes.Count; j++)
        {
            recipeManager.AddRecipe(recipes[j].recipe);
        }

        CollectData();
    }
}
