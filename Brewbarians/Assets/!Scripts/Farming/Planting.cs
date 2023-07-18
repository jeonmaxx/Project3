using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public enum FieldStates { None, Hoed, Wet}
public enum PlantStates { None, Phase01, Phase02, Phase03}
public class Planting : MonoBehaviour, IPointerDownHandler
{
    [Header("States")]
    [ShowOnly]
    public FieldStates curFieldState;
    [ShowOnly]
    //public string currentPlantState;
    //[HideInInspector] public string[] plantStates = { "noPlant", "Phase01", "Phase02", "Phase03" };
    public PlantStates curPlantState;
    
    [Header("Manager")]
    public HandManager handManager;
    public InventoryManager inventoryManager;
    public GameObject seedWheel;

    [Header("Fields")]
    [SerializeField] private GameObject HoedField;
    [SerializeField] private GameObject WetField;
    [HideInInspector] public GameObject hoed;
    [HideInInspector] public GameObject wet;

    [Header("Planting")]
    public bool seedInHand;
    private ActionType handType;
    private bool inHand = false;
    public Seed seed;
    [SerializeField] private GameObject sign;
    private FarmSign farmSign;
    [SerializeField] private GameObject plant;
    private Item currentPlant;

    [Header("Tools")]
    [SerializeField] private Item shovelItem;
    [SerializeField] private Item waterItem;
    [SerializeField] private Item harvestItem;    

    public void Start()
    {
        curFieldState = FieldStates.None;
        curPlantState = PlantStates.None;

        farmSign = sign.GetComponent<FarmSign>();
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
        if(curFieldState == FieldStates.None 
            && handManager.handItem == shovelItem)
        {
            hoed = Instantiate(HoedField, gameObject.transform.position, gameObject.transform.rotation, this.transform);
            curFieldState = FieldStates.Hoed;
        }
    }

    public void WaterField()
    {
        if (curFieldState == FieldStates.Hoed
            && handManager.handItem == waterItem)
        {
            wet = Instantiate(WetField, gameObject.transform.position, gameObject.transform.rotation, this.transform);
            curFieldState = FieldStates.Wet;
        }
    }

    public void PlantField()
    {
        InventoryItem[] seedItem;
        seedItem = seedWheel.GetComponentsInChildren<InventoryItem>();

        for (int i = 0; i < seedItem.Length; i++)
        {
            if ((curFieldState == FieldStates.Hoed
                || curFieldState == FieldStates.Wet)
                && curPlantState == PlantStates.None
                && handManager.handItem.actionType == ActionType.Plant
                && farmSign.signSeed != null)
            {
                seed = farmSign.signSeed.seed;

                if (seedItem[i] != null
                    && seedItem[i].item == farmSign.signSeed)
                {
                    //planted und rechnet einen seed count runter
                    plant = Instantiate(seed.Ph01, gameObject.transform.position, gameObject.transform.rotation, this.transform);
                    curPlantState = PlantStates.Phase01;
                    seedItem[i].count--;

                    //wenn der counter bei 0 ist, wird das childobject gelöscht
                    if (seedItem[i].count <= 0)
                    {
                        Destroy(seedItem[i].gameObject);
                        return;
                    }
                    else
                    {
                        seedItem[i].RefreshCount();
                        return;
                    }
                }
            }
        }
    }

    public void PlantGrowing()
    {
        if(curPlantState == PlantStates.Phase02)
        {
            Destroy(plant);
            plant = Instantiate(seed.Ph02, gameObject.transform.position, gameObject.transform.rotation, this.transform);
        }

        if (curPlantState == PlantStates.Phase03)
        {
            Destroy(plant);
            plant = Instantiate(seed.Ph03, gameObject.transform.position, gameObject.transform.rotation, this.transform);
        }
    }

    public void Harvest()
    {
        if(curPlantState == PlantStates.Phase03
            && handManager.handItem == harvestItem)
        {
            inventoryManager.AddItem(seed.Product);
            Destroy(plant);
            curPlantState = PlantStates.None;
        }
    }

    public void DestroyField()
    {
        if (handManager.handItem == shovelItem
            && (curPlantState != PlantStates.None 
            && curPlantState != PlantStates.Phase03)
            && currentPlant != farmSign.signSeed)
        {
            Destroy(plant);
            curPlantState = PlantStates.None;
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
