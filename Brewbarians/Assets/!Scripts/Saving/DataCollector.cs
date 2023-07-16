using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

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

[System.Serializable]
public class MainSeeds
{
    public Item MainSeed;
    public int Counter;
    public MainSeeds(Item mainSeed, int count)
    {
        MainSeed = mainSeed;
        Counter = count;
    }
}

[System.Serializable]
public class PlantStage
{
    public Seed Seed;
    public int CurrentGrowPoints;

    public PlantStage(Seed seed, int currentGrowPoints)
    { 
        Seed = seed;
        CurrentGrowPoints = currentGrowPoints; 
    }
}

public class DataCollector : MonoBehaviour
{
    [Header("Input")]
    public PlayerMovement playerMovement;
    public InventoryManager inventoryManager;
    public RecipeManager recipeManager;
    //Wie verwaltet man am besten die Pflanzen in einer anderen Szene? 
    //Ein Script was nur die Punkte verwaltet und erst wenn man in der Szene ist auf die Objekte zugreift?
    //Die Felder müssem auch verwaltet werden !
    // "FieldPlantManager" ?

    [Header("Data")]
    public Vector3 playerPosition;
    public List<MainItems> mainItems;
    public List<MainSeeds> mainSeeds;
    public List<Recipe> recipes;
    public List<PlantStage> plants;

    private InventoryItem tmpInven;
    private int tmpCount;

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
        mainSeeds = new List<MainSeeds>();
        for (int i = 0; i < inventoryManager.seedWheel.Length; i++)
        {
            if (inventoryManager.seedWheel[i].transform.childCount != 0)
            {
                tmpInven = inventoryManager.seedWheel[i].transform.GetChild(0).GetComponent<InventoryItem>();
                mainSeeds.Add(new MainSeeds(tmpInven.item, tmpInven.count));
            }
            else
            {
                mainSeeds.Add(null);
            }
        }

        //Recipes
        recipes = new List<Recipe>();
        if (recipeManager.recipeHolder.transform.childCount != 0 && recipeManager.recipeHolder.transform.childCount > recipes.Count)
        {   
            for (int i = 0; i < recipeManager.recipeHolder.transform.childCount; i++)
            {
                recipes.Add(recipeManager.recipeHolder.transform.GetChild(i).GetComponent<RecipeItem>().recipe);
            }
        }
    }


    public void GiveData()
    {
        //Changes Player Position
        playerMovement.gameObject.transform.position = playerPosition;

        LoadItems();
        LoadSeeds();
        LoadRecipes();
    }

    public void LoadItems()
    {
        DeleteItems();
        GiveItems();
    }

    public void LoadSeeds()
    {
        DeleteSeeds();
        GiveSeeds();
    }

    public void LoadRecipes()
    {
        DeleteRecipes();
        GiveRecipes();
    }

    private void DeleteItems()
    {
        for (int i = 0; i < inventoryManager.inventorySlots.Length; i++)
        {
            if (inventoryManager.inventorySlots[i].transform.childCount != 0)
            {
                Destroy(inventoryManager.inventorySlots[i].transform.GetChild(0).gameObject);
            }
        }
    }
    private void GiveItems()
    {
        for (int i = 0; i < mainItems.Count; i++)
        {
            InventoryItem inventoryItem = null;
            tmpCount = mainItems[i].Counter;
            for (int j = 0; j < tmpCount; j++)
            {
                if (j == 0)
                {
                    GameObject newItemGo = Instantiate(inventoryManager.inventoryItemPrefab, inventoryManager.inventorySlots[i].transform);
                    inventoryItem = newItemGo.GetComponent<InventoryItem>();
                    inventoryItem.InitialiseItem(mainItems[i].MainItem);
                }
                else if (j > 0)
                {
                    inventoryItem.count++;
                    inventoryItem.RefreshCount();
                }
            }
        }
    }

    private void DeleteSeeds()
    {
        for (int i = 0; i < inventoryManager.seedWheel.Length; i++)
        {
            if (inventoryManager.seedWheel[i].transform.childCount != 0)
            {
                Destroy(inventoryManager.seedWheel[i].transform.GetChild(0).gameObject);
            }
        }
    }
    private void GiveSeeds()
    {
        for (int i = 0; i < mainSeeds.Count; i++)
        {
            InventoryItem inventoryItem = null;
            tmpCount = mainSeeds[i].Counter;
            for (int j = 0; j < tmpCount; j++)
            {
                if (j == 0)
                {
                    GameObject newItemGo = Instantiate(inventoryManager.inventoryItemPrefab, inventoryManager.seedWheel[i].transform);
                    inventoryItem = newItemGo.GetComponent<InventoryItem>();
                    inventoryItem.InitialiseItem(mainSeeds[i].MainSeed);
                }
                else if (j > 0)
                {
                    inventoryItem.count++;
                    inventoryItem.RefreshCount();
                }
            }
        }
    }

    private void DeleteRecipes()
    {
        for (int i = 0; i < recipeManager.recipeHolder.transform.childCount && recipeManager.recipeHolder.transform.childCount != 0; i++)
        {
            Destroy(recipeManager.recipeHolder.transform.GetChild(i).gameObject);
        }
    }
    private void GiveRecipes()
    {
        for (int j = 0; j < recipes.Count; j++)
        {
            recipeManager.AddRecipe(recipes[j]);
        }
    }
}
