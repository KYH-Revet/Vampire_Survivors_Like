using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // List of enemy prefabs to spawn
    public List<GameObject> enemyPool;
    float spawnTime = 0f;

    private void Awake()
    {
        enemyPool = new List<GameObject>();
    }
    void Update()
    {
        spawnTime += Time.deltaTime;
        if(spawnTime >= 5f)
        {
            spawnTime = 0f;
            SpawnByTime();
        }
    }

    // Function to spawn enemies based on elapsed game time
    void SpawnByTime()
    {
        int maxEnemies = 0;
        int enemyCount = 0;

        // Determine the maximum number of enemies and type based on play time
        switch (GameManager.instance.playTime)
        {
            case < 10:
                maxEnemies = 2;
                enemyCount = 0;
                break;
            case < 20:
                maxEnemies = 5;
                enemyCount = 0;
                break;
            case < 30:
                maxEnemies = 10;
                enemyCount = 0;
                break;
            default:
                maxEnemies = 15;
                enemyCount = 0;
                break;
        }

        // Spawn enemies at random positions on a circle
        for (int i = 0; i < maxEnemies; i++)
        {
            Vector2 spawnPosition = RandomOnCircle(Player.instance.transform.position, 10f);
            SpawnEnemy(spawnPosition, enemyCount);
        }
    }

    // Function to get a random position on the circumference of a circle
    Vector2 RandomOnCircle(Vector2 center, float r)
    {
        float theta = Random.Range(0f, 2f * Mathf.PI);
        return center + new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * r;
    }
    // Function to spawn an enemy at a given position
    public void SpawnEnemy(Vector3 position, int enemyCount)
    {
        if (enemyPrefabs == null || enemyPrefabs.Count == 0)
        {
            Debug.LogWarning("Enemy prefabs list is empty or not assigned.");
            return;
        }

        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                Debug.Log(enemy.name + " is Reusing enemy from pool.");
                enemy.transform.position = position;
                enemy.SetActive(true);
                enemy.GetComponent<Enemy>().Initialize();
                return;
            }
        }

        // Instantiate the selected enemy at the specified position with no rotation
        GameObject newEnemy = Instantiate(enemyPrefabs[enemyCount], position, Quaternion.identity);
        // Set the parent of the newly spawned enemy to the enemy pool for organization
        newEnemy.transform.SetParent(transform);
        // Add the new enemy to the pool
        enemyPool.Add(newEnemy);
    }
}
