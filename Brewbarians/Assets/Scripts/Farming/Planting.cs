using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class Planting : MonoBehaviour, IPointerDownHandler
{
    [Header("States")]
    public string currentFieldState;
    [HideInInspector] public string[] fieldStates = { "default", "hoed", "wet" };
    public string currentPlantState;
    [HideInInspector] public string[] plantStates = { "noPlant", "Phase01", "Phase02", "Phase03" };
    
    [Header("Manager")]
    public HandManager handManager;
    public InventoryManager inventoryManager;

    [Header("Fields")]
    [SerializeField] private GameObject HoedField;
    [SerializeField] private GameObject WetField;

    [Header("Planting")]
    public bool seedInHand;
    private ActionType handType;
    private bool inHand = false;
    private Seed seed;
    public GameObject sign;
    private FarmSign farmSign;

    [Header("Tools")]
    public Item shovelItem;
    public Item waterItem;
    public Item harvestItem;

    private GameObject plantFolder;

    public void Start()
    {
        currentFieldState = fieldStates[0];
        currentPlantState = plantStates[0];

        farmSign = sign.GetComponent<FarmSign>();

        plantFolder = transform.GetChild(0).gameObject;
    }

    public void Update()
    {
        CheckHand();
        PlantGrowing();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ShovelField();
        WaterField();
        PlantField();
        Harvest();
    }

    public void ShovelField()
    {
        if (currentFieldState == fieldStates[0] 
            && handManager.handItem == shovelItem)
        {
            Instantiate(HoedField, gameObject.transform.position, gameObject.transform.rotation, this.transform);
            currentFieldState = fieldStates[1];
        }
    }

    public void WaterField()
    {
        if (currentFieldState == fieldStates[1] 
            && handManager.handItem == waterItem)
        {
            Instantiate(WetField, gameObject.transform.position, gameObject.transform.rotation, this.transform);
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
                && handManager.handItem == farmSign.signSeed)
            {
                Instantiate(seed.Ph01, gameObject.transform.position, gameObject.transform.rotation, plantFolder.transform);
                currentPlantState = plantStates[1];
                Item receivedItem = inventoryManager.GetSelectedItem(true);
            }
        }
    }

    public void PlantGrowing()
    {
        if(currentPlantState == plantStates[2])
        {
            Destroy(plantFolder.transform.GetChild(0).gameObject);
            Instantiate(seed.Ph02, gameObject.transform.position, gameObject.transform.rotation, plantFolder.transform);
        }

        if (currentPlantState == plantStates[3])
        {
            Destroy(plantFolder.transform.GetChild(0).gameObject);
            Instantiate(seed.Ph03, gameObject.transform.position, gameObject.transform.rotation, plantFolder.transform);
        }
    }

    public void Harvest()
    {
        if(currentPlantState == plantStates[3]
            && handManager.handItem == harvestItem)
        {
            //Item prod = seed.Product;

            bool result = inventoryManager.AddItem(seed.Product);
            Destroy(plantFolder.transform.GetChild(0).gameObject);
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
