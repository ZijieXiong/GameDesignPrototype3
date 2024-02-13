using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject keyPrefab;
    public Transform[] spawnPoints;
    public int numberOfKeys = 3;

    private void Start()
    {
        SpawnKeys();
    }

    private void SpawnKeys()
    {
        bool[] usedSpawnPoints = new bool[spawnPoints.Length]; // Keep track of used spawn points
        for (int i = 0; i < numberOfKeys; i++)
        {
            int randomSpawnIndex = GetRandomUnusedSpawnIndex(usedSpawnPoints);
            usedSpawnPoints[randomSpawnIndex] = true; // Mark the spawn point as used
            Transform spawnPoint = spawnPoints[randomSpawnIndex];
            GameObject key = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
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
