using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTester : MonoBehaviour
{
    public PointsCollector collector;
    public void SceneChangeButton(int index)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            collector.addedFarmPoints = 0;
        if(SceneManager.GetActiveScene().buildIndex == 2)
            collector.addedBrewPoints = 0;

        SceneManager.LoadScene(index);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
