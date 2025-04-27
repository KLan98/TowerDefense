using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 100;
    public float spawnInterval = 5f;

    private List<GameObject> enemyPool;
    private int enemiesSpawned = 0;

    private void Start()
    {
        InitEnemyPool();
    }

    // as long as long enemies spawned < pool size keep spawning at spawn position
    private IEnumerator SpawnEnemy()
    {
        while (enemiesSpawned < poolSize)
        {
            yield return new WaitForSeconds(spawnInterval);
            GameObject enemyToSpawn = enemyPool[enemiesSpawned];
            enemyToSpawn.SetActive(true);
            enemyToSpawn.transform.position = GetSpawnPosition();
            enemiesSpawned++;
        }
    }

    private void InitEnemyPool()
    {
        // init enemy pool and then spawn enemies 
        enemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab); // Clones the object enemyPrefab and returns the clone. 
            enemy.SetActive(false); // set it to inactive
            enemy.transform.SetParent(this.transform); 
            enemyPool.Add(enemy); // add game object to pool
        }

        StartCoroutine(SpawnEnemy());   
    }

    private Vector3 GetSpawnPosition()
    {
        // Simple example: random position around (0, 0, 0)
        return new Vector3(71, 0, -10);
    }
}
