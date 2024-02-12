using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int numberOfEnemies = 3;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnIndex];

            // Check if the spawn point is clear
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint.position, 0.1f);
            bool isSpawnPointClear = true;
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    isSpawnPointClear = false;
                    break;
                }
            }

            if (isSpawnPointClear)
            {
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
            else
            {
                // Retry spawning at another point
                i--; // Decrease i to repeat the loop iteration
            }
        }
    }
}
