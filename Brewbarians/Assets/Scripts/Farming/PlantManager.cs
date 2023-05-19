using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class PlantManager : PlayerNear
{
    public HandManager handManager;
    private ActionType handType;
    private bool inHand = false;
    public GameObject[] fields;
    public bool seedInHand;
    public PlayerInput input;
    private PlantState plantState;
    private FarmingField fieldState;
    private Seed seed;
    public InventoryManager inventoryManager;

    public void Update()
    {
        CheckHand();
        CalcDistance();
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


        if ((int)handType == 2 && inHand)
            seedInHand = true;
        else if ((int)handType != 2 || !inHand)
            seedInHand = false;

    }

    public void OnInteract()
    {
        int seedCount = handManager.count;

        if (isPlayerNear && seedInHand)
        {
            for(int i = 0; i < fields.Length; i++)
            {
                plantState = fields[i].GetComponent<PlantState>();
                fieldState = fields[i].GetComponent<FarmingField>();

                if (!seedInHand)
                     return;

                if (plantState.currentPlantState == plantState.plantStates[0] &&
                   (fieldState.currentFieldState == fieldState.fieldStates[1] || 
                    fieldState.currentFieldState == fieldState.fieldStates[2]) &&
                    seedCount > 0)
                {
                    //if (!seedInHand)
                    //    return;

                    Debug.Log("Ready to plant!");
                    seed = handManager.handItem.seed;
                    Instantiate(seed.Ph01, fields[i].transform.position, fields[i].transform.rotation, fields[i].transform);
                    Item receivedItem = inventoryManager.GetSelectedItem(true);
                    seedCount -= 1;
                    plantState.currentPlantState = plantState.plantStates[1];
                }
                else
                {
                    Debug.Log("Not enough seeds/space");
                }

                //ToDo:
                //Get itemCount von den Seeds in der Hand
                //Bei jedem wo PlantState[0] ist planten, solange man noch Seeds hat
                //Fields, welche einen anderen PlantState haben überspringen
            }
        }
    }
}
