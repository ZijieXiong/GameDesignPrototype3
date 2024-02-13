using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyScript : MonoBehaviour
{
    public GameObject keyPrefab;
    public Transform[] spawnPoints;
    public int numberOfKeys = 3;

    private void Start()
    {
        //Spawn key only if the player haven't found the key in this scene
        string currentScene = SceneManager.GetActiveScene().name;
        int keyFound = PlayerPrefs.GetInt(currentScene+"KeyFound", 0);
        Debug.Log("keyFound:");
        Debug.Log(keyFound);
        if(keyFound<=0)
        {
            SpawnKeys();
        }
    }

    private void SpawnKeys()
    {
        bool[] usedSpawnPoints = new bool[spawnPoints.Length]; // Keep track of used spawn points
        for (int i = 0; i < numberOfKeys; i++)
        {
            int randomSpawnIndex = GetRandomUnusedSpawnIndex(usedSpawnPoints);
            usedSpawnPoints[randomSpawnIndex] = true; // Mark the spawn point as used
            Transform spawnPoint = spawnPoints[randomSpawnIndex];
            GameObject key = Instantiate(keyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    private int GetRandomUnusedSpawnIndex(bool[] usedSpawnPoints)
    {
        // Find a random unused spawn point index
        int randomSpawnIndex;
        do
        {
            randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        } while (usedSpawnPoints[randomSpawnIndex]); // Repeat until an unused spawn point is found
        return randomSpawnIndex;
    }
}
