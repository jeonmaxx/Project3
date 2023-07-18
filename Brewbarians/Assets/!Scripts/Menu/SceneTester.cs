using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTester : MonoBehaviour
{
    public PointsCollector collector;
    public int tmpIndex = 1;

    public void LoadGame()
    {
        string filePath = Application.persistentDataPath + "/" + "scene.json";
        if (File.Exists(filePath))
        {
            Vector2 tmp = SaveGameManager.ReadFromJSON<Vector2>("scene.json");
            tmpIndex = (int)tmp.x;
        }
        SceneManager.LoadScene(tmpIndex);
    }

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
