using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTester : MonoBehaviour
{
    public DataCollector dataCollector;
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
        dataCollector.CollectData();
        SceneManager.LoadScene(index);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
