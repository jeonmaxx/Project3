using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public Text countText;

    [HideInInspector] public Item item;
    public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    public Slider waterSlider;

    public void Update()
    {
        if (item != null && item.actionType == ActionType.Water)
        {
            waterSlider.value = item.currentWater;
            waterSlider.maxValue = item.waterAmount;
        }
    }

    public void InitialiseItem(Item newItem)
    {
        if(newItem.actionType == ActionType.Water)
        {
            waterSlider.gameObject.SetActive(true);
            waterSlider.value = newItem.currentWater;
            waterSlider.maxValue = newItem.waterAmount;
        }
        else
        {
            waterSlider.gameObject.SetActive(false);
        }
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    // Drag and Drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
