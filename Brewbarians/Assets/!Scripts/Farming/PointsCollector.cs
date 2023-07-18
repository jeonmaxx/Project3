using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCollector : MonoBehaviour
{
    public float addedFarmPoints;
    public float addedBrewPoints;

    public void AddFarmPoints()
    {
        addedFarmPoints++;
    }

    public void AddBrewPoints()
    {
        addedBrewPoints++;
    }
}
