using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCollector : MonoBehaviour
{
    public int addedFarmPoints;
    public int addedBrewPoints;

    public void AddFarmPoints()
    {
        addedFarmPoints++;
    }

    public void AddBrewPoints()
    {
        addedBrewPoints++;
    }
}
