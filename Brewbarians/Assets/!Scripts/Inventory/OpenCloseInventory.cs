using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseInventory : MonoBehaviour
{
    private OpenInventory inventory;
    [SerializeField] private GameObject manager;

    public void Inventory()
    {
        inventory = manager.GetComponent<OpenInventory>();
        inventory.windows = Windows.Recipe;
        inventory.inventoryActive = true;
    }
}
