using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FarmingField : MonoBehaviour, IPointerDownHandler
{
    [Header("Field Types")]
    [SerializeField] private GameObject HoedField;
    [SerializeField] private GameObject WetField;
    public Item shovelItem;
    public Item waterItem;
    public HandManager handManager;

    private string[] fieldStates = { "default", "hoed", "wet" };

    public string currentFieldState;

    public void Awake()
    {
        currentFieldState = fieldStates[0];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ShovelField();
        WaterField();
    }

    public void ShovelField()
    {
        if(currentFieldState == fieldStates[0] && handManager.handItem == shovelItem )
        {
            Instantiate(HoedField, gameObject.transform.position, gameObject.transform.rotation, this.transform);
            currentFieldState = fieldStates[1];
        }
    }

    public void WaterField()
    {
        if (currentFieldState == fieldStates[1] && handManager.handItem == waterItem)
        {
            Instantiate(WetField, gameObject.transform.position, gameObject.transform.rotation, this.transform);
            currentFieldState = fieldStates[2];
        }
    }

}
