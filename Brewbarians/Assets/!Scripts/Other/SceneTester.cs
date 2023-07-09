using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTester : MonoBehaviour
{
    public void SceneChangeButton(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
