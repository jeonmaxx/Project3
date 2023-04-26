using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FarmingField : MonoBehaviour, IPointerDownHandler
{
    [Header("Field Types")]
    [SerializeField] private GameObject HoedField;
    [SerializeField] private GameObject WetField;

    private string[] fieldStates = { "default", "hoed", "wet" };

    public string currentFieldState;

    public void Awake()
    {
        currentFieldState = fieldStates[0];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HoeField();
        WaterField();
    }

    public void HoeField()
    {
        if(currentFieldState == fieldStates[0])
        {
            Instantiate(HoedField, gameObject.transform.position, gameObject.transform.rotation);
            currentFieldState = fieldStates[1];
        }
    }

    public void WaterField()
    {
        if (currentFieldState == fieldStates[1])
        {
            Instantiate(WetField, gameObject.transform.position, gameObject.transform.rotation);
            currentFieldState = fieldStates[2];
        }
    }

}
