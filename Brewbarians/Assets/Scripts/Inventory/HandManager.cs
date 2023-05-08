using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item handItem;

    public void Update()
    {
        GetHandItem();
    }

    public void GetHandItem()
    {
        Item selectedItem = inventoryManager.GetSelectedItem(false);
        if (selectedItem != null)
        {
            handItem = selectedItem;
        }
        else
        {
            handItem = null;
        }
    }
}
