using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growing : MonoBehaviour
{
    [Header("Growing Points")]
    public int phase2Points = 5;
    public int phase3Points = 10;
    public int currentPoints;

    private Planting planting;

    public void Start()
    {
        planting = GetComponent<Planting>();
    }

    public void Update()
    {
        CheckPlantState();
    }

    public void PlusPoint()
    {
        if (planting.currentPlantState != planting.plantStates[0] 
            && currentPoints < phase3Points
            && planting.currentFieldState == planting.fieldStates[2])
        {
            currentPoints++;
        }
    }

    public void CheckPlantState()
    {
        if (planting.currentPlantState != planting.plantStates[0])
        {
            switch (currentPoints)
            {
                case 0:
                    planting.currentPlantState = planting.plantStates[1];
                    break;
                case 5:
                    planting.currentPlantState = planting.plantStates[2];
                    break;
                case 10:
                    planting.currentPlantState = planting.plantStates[3];
                    break;
            }
        }

        if(planting.currentPlantState == planting.plantStates[0])
            currentPoints = 0;
    }
}
