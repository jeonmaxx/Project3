using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class OpenInventory : MonoBehaviour
{
    public PlayerInput input;
    public bool inventoryActive = false;
    public GameObject inventoryBox;
    public GameObject toolBar;
    [HideInInspector] public Vector3 toolBarPos;
    public Vector3 newToolBarPos;
    [ShowOnly] public Windows windows;

    public void Start()
    {
        toolBarPos = new Vector3(toolBar.GetComponent<RectTransform>().anchoredPosition.x, toolBar.GetComponent<RectTransform>().anchoredPosition.y, 0);
        windows = Windows.Inventory;
    }

    public void OnInventory()
    {
        if(inventoryActive == false)
        {
            inventoryBox.GetComponent<Canvas>().enabled = true;

            if(windows == Windows.Inventory)
                toolBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(newToolBarPos.x, newToolBarPos.y, 0);

            inventoryActive = true;
        }
        else
        {
            inventoryBox.GetComponent<Canvas>().enabled = false;
            toolBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(toolBarPos.x, toolBarPos.y, 0);
            inventoryActive = false;
        }
    }
}
