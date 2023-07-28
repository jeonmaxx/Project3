using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

public class OpenInventory : MonoBehaviour
{
    public PlayerInput input;
    public bool inventoryActive = false;
    public GameObject inventoryBox;
    public GameObject toolBar;
    [HideInInspector] public Vector3 toolBarPos;
    public Vector3 newToolBarPos;
    [ShowOnly] public Windows windows;

    public Sprite slotImageUp;
    public Sprite slotImageDown;

    public RectTransform mainInventory;
    public RectTransform map;
    public RectTransform recipe;

    private Color color;

    public void Start()
    {
        toolBarPos = new Vector3(toolBar.GetComponent<RectTransform>().anchoredPosition.x, toolBar.GetComponent<RectTransform>().anchoredPosition.y, 0);
        windows = Windows.Inventory;

        color = toolBar.GetComponent<Image>().color;
    }

    public void Update()
    {
        if (!inventoryActive)
        {
            inventoryBox.GetComponent<Canvas>().enabled = false;
            toolBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(toolBarPos.x, toolBarPos.y, 0);
            for(int i = 0; i < toolBar.transform.childCount; i++)
            {
                toolBar.transform.GetChild(i).GetComponent<Image>().sprite = slotImageDown;
            }

            toolBar.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 100);
        }
        else
        {
            inventoryBox.GetComponent<Canvas>().enabled = true;

            toolBar.GetComponent<RectTransform>().anchoredPosition = new Vector3(newToolBarPos.x, newToolBarPos.y, 0);
            for (int i = 0; i < toolBar.transform.childCount; i++)
            {
                toolBar.transform.GetChild(i).GetComponent<Image>().sprite = slotImageUp;
            }

            toolBar.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0);
        }

        switch (windows)
        {
            case Windows.Inventory:
                mainInventory.SetSiblingIndex(6);
                break;
            //case Windows.Map:
            //    map.SetSiblingIndex(6);
            //    break;
            case Windows.Recipe:
                recipe.SetSiblingIndex(6);
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
