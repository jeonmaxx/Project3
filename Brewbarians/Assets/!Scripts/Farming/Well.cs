using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Well : PlayerNear, IPointerDownHandler
{
    public Item waterItem;
    public HandManager handManager;

    public void Update()
    {
        CalcDistance();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RefillWater();
    }

    public void RefillWater()
    {
        if(isPlayerNear && handManager.handItem == waterItem)
        {
            waterItem.currentWater = waterItem.waterAmount;
        }
    }
}
