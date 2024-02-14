using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int numberOfEnemies = 1;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        bool[] usedSpawnPoints = new bool[spawnPoints.Length]; // Keep track of used spawn points
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int randomSpawnIndex = GetRandomUnusedSpawnIndex(usedSpawnPoints);
            usedSpawnPoints[randomSpawnIndex] = true; // Mark the spawn point as used
            Transform spawnPoint = spawnPoints[randomSpawnIndex];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
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