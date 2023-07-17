using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Growing : MonoBehaviour
{
    [Header("Growing Points")]
    public int phase2Points = 5;
    public int phase3Points = 10;
    public int currentGrowPoints;

    [Header("Field Points")]
    public int hoedPoints = 5;
    [ShowOnly] 
    public int currentHoedPoints;
    public int wetPoints = 5;
    [ShowOnly]
    public int currentWetPoints;

    private Planting planting;
    public PointsCollector collector;

    public void Start()
    {
        planting = GetComponent<Planting>();
        PlusPoint();
    }

    public void Update()
    {
        CheckPlantState();
        CheckFieldStates();
    }

    public void PlusPoint()
    {
        int tmpPoints = collector.addedFarmPoints;

        if (planting.currentPlantState != planting.plantStates[0] 
            && currentGrowPoints < phase3Points
            && planting.currentFieldState == planting.fieldStates[2])
        {
            if (tmpPoints + currentWetPoints <= wetPoints)
            {
                currentGrowPoints += tmpPoints;
            }
            else
            {
                currentGrowPoints += (wetPoints - currentWetPoints);
            }
        }

        if (planting.currentFieldState == planting.fieldStates[1]
            && planting.currentPlantState == planting.plantStates[0])
        {
            currentHoedPoints += tmpPoints;
        }

        if (planting.currentFieldState == planting.fieldStates[2]
            && currentWetPoints < wetPoints)
        {
            currentWetPoints += tmpPoints;
        }
    }

    public void CheckPlantState()
    {
        if (planting.currentPlantState != planting.plantStates[0])
        {
            if(currentGrowPoints < phase2Points)
            {
                planting.currentPlantState = planting.plantStates[1];
            }
            else if(currentGrowPoints >= phase2Points && currentGrowPoints < phase3Points)
            {
                planting.currentPlantState = planting.plantStates[2];
            }
            else if(currentGrowPoints >= phase3Points)
            {
                planting.currentPlantState = planting.plantStates[3];
            }
        }

        if(planting.currentPlantState == planting.plantStates[0])
            currentGrowPoints = 0;
    }

    public void CheckFieldStates()
    {
        if(currentHoedPoints >= hoedPoints)
        {
            currentHoedPoints = 0;
            planting.currentFieldState = planting.fieldStates[0];
            Destroy(planting.hoed);
        }

        if(currentWetPoints >= wetPoints)
        {
            currentWetPoints = 0;
            planting.currentFieldState = planting.fieldStates[1];
            Destroy(planting.wet);
        }
    }
}
