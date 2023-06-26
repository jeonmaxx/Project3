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

    public RectTransform mainInventory;
    public RectTransform map;
    public RectTransform recipe;

    public void Start()
    {
        toolBarPos = new Vector3(toolBar.GetComponent<RectTransform>().anchoredPosition.x, toolBar.GetComponent<RectTransform>().anchoredPosition.y, 0);
        windows = Windows.Inventory;
    }

    public void Update()
    {
        if (!inventoryActive)
        {
            inventoryBox.GetComponent<Canvas>().enabled = false;
            toolBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(toolBarPos.x, toolBarPos.y, 0);
        }
        else
        {
            inventoryBox.GetComponent<Canvas>().enabled = true;

            if (windows == Windows.Inventory)
                toolBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(newToolBarPos.x, newToolBarPos.y, 0);
        }

        switch (windows)
        {
            case Windows.Inventory:
                recipe.SetSiblingIndex(1);
                break;
            //case Windows.Map:
            //    map.SetSiblingIndex(1);
            //    break;
            case Windows.Recipe:
                mainInventory.SetSiblingIndex(1);
                break;
        }
    }

    public void OnInventory()
    {
        if(!inventoryActive)
        {
            inventoryActive = true;
        }
        else
        {
            inventoryActive = false;
        }
    }
}
