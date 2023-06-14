using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;

    private ToolBarNumber toolBarNumber;
    public InventoryManager inventoryManager;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }

    // Drag and Drop
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }

    //Item Selection
    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.parent.gameObject.CompareTag("Toolbar"))
        {
            toolBarNumber = GetComponent<ToolBarNumber>();
            inventoryManager.ChangeSelectedSlot(toolBarNumber.number);
        }
    }
}
