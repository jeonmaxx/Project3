using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public HandManager handManager;
    private ItemType handType;

    public void Update()
    {
        if (handManager.handItem != null)
            handType = handManager.handItem.type;


        if((int)handType == 2)
        {
            Debug.Log("Seed in hand");
        }
    }
}
