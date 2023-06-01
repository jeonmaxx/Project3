using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class Planting : MonoBehaviour, IPointerDownHandler
{
    [Header("States")]
    [ShowOnly]
    public string currentFieldState;
    [HideInInspector] public string[] fieldStates = { "default", "hoed", "wet" };
    [ShowOnly]
    public string currentPlantState;
    [HideInInspector] public string[] plantStates = { "noPlant", "Phase01", "Phase02", "Phase03" };
    
    [Header("Manager")]
    public HandManager handManager;
    public InventoryManager inventoryManager;
    public GameObject seedWheel;
    private SeedWheelManager seedWheelManager;

    [Header("Fields")]
    [SerializeField] private GameObject HoedField;
    [SerializeField] private GameObject WetField;
    [HideInInspector] public GameObject hoed;
    [HideInInspector] public GameObject wet;

    [Header("Planting")]
    public bool seedInHand;
    private ActionType handType;
    private bool inHand = false;
    private Seed seed;
    [SerializeField] private GameObject sign;
    private FarmSign farmSign;
    private GameObject plant;
    private Item currentPlant;

    [Header("Tools")]
    [SerializeField] private Item shovelItem;
    [SerializeField] private Item waterItem;
    [SerializeField] private Item harvestItem;    

    public void Start()
    {
        currentFieldState = fieldStates[0];
        currentPlantState = plantStates[0];

        farmSign = sign.GetComponent<FarmSign>();
        seedWheelManager = seedWheel.GetComponent<SeedWheelManager>();
    }

    public void Update()
    {
        CheckHand();
        PlantGrowing();
        currentPlant = farmSign.signSeed;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ShovelField();
        WaterField();
        PlantField();
        Harvest();
        DestroyField();
    }

    public void ShovelField()
    {
        if (currentFieldState == fieldStates[0] 
            && handManager.handItem == shovelItem)
        {
            hoed = Instantiate(HoedField, gameObject.transform.position, gameObject.transform.rotation, this.transform);
            currentFieldState = fieldStates[1];
        }
    }

    public void WaterField()
    {
        if (currentFieldState == fieldStates[1] 
            && handManager.handItem == waterItem)
        {
            wet = Instantiate(WetField, gameObject.transform.position, gameObject.transform.rotation, this.transform);
            currentFieldState = fieldStates[2];
        }
    }

    public void PlantField()
    {
        if (farmSign.signSeed != null)
        {
            seed = farmSign.signSeed.seed;
            if ((currentFieldState == fieldStates[1]
                || currentFieldState == fieldStates[2])
                && currentPlantState == plantStates[0]
                && handManager.handItem.actionType == ActionType.Plant
                && seedWheelManager.chosenSeed != null
                && farmSign.signSeed == seedWheelManager.chosenSeed)
            {
                plant = Instantiate(seed.Ph01, gameObject.transform.position, gameObject.transform.rotation, this.transform);
                currentPlantState = plantStates[1];

                //Uses Seeds
                //nimmt nicht die Seeds die auf dem Schild sind sondern irgendwelche. !!!! Beheben !!!!!
                InventoryItem seedItem;
                seedItem = seedWheel.GetComponentInChildren<InventoryItem>();
                seedItem.count--;
                if (seedItem.count <= 0)
                {
                    Destroy(seedItem.gameObject);
                }
                else
                {
                    seedItem.RefreshCount();
                }
            }
        }
    }

    public void PlantGrowing()
    {
        if(currentPlantState == plantStates[2])
        {
            Destroy(plant);
            plant = Instantiate(seed.Ph02, gameObject.transform.position, gameObject.transform.rotation, this.transform);
        }

        if (currentPlantState == plantStates[3])
        {
            Destroy(plant);
            plant = Instantiate(seed.Ph03, gameObject.transform.position, gameObject.transform.rotation, this.transform);
        }
    }

    public void Harvest()
    {
        if(currentPlantState == plantStates[3]
            && handManager.handItem == harvestItem)
        {
            //Item prod = seed.Product;

            bool result = inventoryManager.AddItem(seed.Product);
            Destroy(plant);
            currentPlantState = plantStates[0];

        }
    }

    public void DestroyField()
    {
        if (handManager.handItem == shovelItem
            && (currentPlantState != plantStates[0] 
            && currentPlantState != plantStates[3])
            && currentPlant != farmSign.signSeed)
        {
            Destroy(plant);
            currentPlantState = plantStates[0];
        }
    }

    public void CheckHand()
    {
        if (handManager.handItem != null)
        {
            handType = handManager.handItem.actionType;
            inHand = true;
        }
        else
            inHand = false;


        if ((int)handType == 3 && inHand)
            seedInHand = true;
        else if ((int)handType != 3 || !inHand)
            seedInHand = false;
    }
}
