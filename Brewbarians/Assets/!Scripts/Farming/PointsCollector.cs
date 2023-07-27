using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCollector : MonoBehaviour
{
    public float addedFarmPoints;
    public float addedBrewPoints;
    public float secondsTilPoints = 5;

    public void Start()
    {
        StartCoroutine(WaitForAdding());
    }

    public void AddFarmPoints()
    {
        addedFarmPoints++;
    }

    public void AddBrewPoints()
    {
        addedBrewPoints++;
    }

    public IEnumerator WaitForAdding()
    {
        yield return new WaitForSeconds(secondsTilPoints);
        AddBrewPoints();
        AddFarmPoints();
        StopCoroutine(WaitForAdding());
    }
}
