using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public int sceneIndex;

    public DataCollector dataCollector;
    public int tmpIndex = 1;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            StartCoroutine(LoadYourAsyncScene(sceneIndex));
    }

    public IEnumerator LoadYourAsyncScene(int scene)
    {
        dataCollector.CollectData();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
