using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class PlantManager : PlayerNear
{
    public HandManager handManager;
    private ActionType handType;
    public GameObject[] fields;
    private bool seedInHand;
    public PlayerInput input;
    private int seedCount;

    public void Update()
    {
        CheckHand();
        CalcDistance();
    }

    public void CheckHand()
    {
        if (handManager.handItem != null)
            handType = handManager.handItem.actionType;


        if ((int)handType == 2)
            seedInHand = true;
        else
            seedInHand = false;

        if (seedInHand)
            Debug.Log("Seed in Hand");
    }

    public void OnInteract()
    {
        if(isPlayerNear)
        {
            for(int i = 0; i <= seedCount && i <= fields.Length; i++)
            {
                //ToDo:
                //Get PlantState von jedem Field
                //Get itemCount von den Seeds in der Hand
                //Bei jedem wo PlantState[0] ist planten, solange man noch Seeds hat
                //Fields, welche einen anderen PlantState haben überspringen
            }
        }
    }


}
