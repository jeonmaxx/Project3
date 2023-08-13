using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SpawnPosition
{
    public int LastSceneIndex;
    public Vector3 NewSpawnPosition;

    public SpawnPosition(int lastSceneIndex, Vector3 newSpawnPosition)
    {
        LastSceneIndex = lastSceneIndex;
        NewSpawnPosition = newSpawnPosition;
    }
}

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    public List<SpawnPosition> spawnPositions;
    private Vector2 sceneData;

    private void Awake()
    {
        string filePath = Application.persistentDataPath + "/" + "scene.json";
        sceneData = SaveGameManager.ReadFromJSON<Vector2>("scene.json");
        if ((!File.Exists(filePath) || new FileInfo(filePath).Length <= 0) && SceneManager.GetActiveScene().buildIndex == 4)
        {
            player.transform.position = new Vector3(-5.9f, 6.6f, 0);
        }

        for (int i = 0; i < spawnPositions.Count; i++)
        {
            Debug.Log("scene" + sceneData.x);
            if (sceneData.x == spawnPositions[i].LastSceneIndex)
            {
                player.transform.position = spawnPositions[i].NewSpawnPosition;
            }
        }
    }
}
