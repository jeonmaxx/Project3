using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantState : MonoBehaviour
{
    private string[] plantStates = {"noPlant", "Phase01", "Phase02", "Phase03"};
    public string currentPlantState;

    public void Start()
    {
        currentPlantState = plantStates[0];
    }
}
