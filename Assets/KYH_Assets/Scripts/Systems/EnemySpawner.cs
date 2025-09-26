using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    enum EnemyPool
    {
        enemy1,
        enemy2,
        enemy3,
        enemy4
    }
    enum SpawnPhase
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4
    }
    SpawnPhase currentPhase = SpawnPhase.Phase1;
    [Header("Enemy Pools")]
    public PooledObject<GameObject> pooledEnemy;

    void Start()
    {
        //enemy = ObjPool_EnemyTest.instance;
    }
    private void Update()
    {
        Phase();
    }

    void Phase()
    {
        switch (currentPhase)
        {
            case SpawnPhase.Phase1:
                
                break;
            case SpawnPhase.Phase2:

                break;
            case SpawnPhase.Phase3:

                break;
            case SpawnPhase.Phase4:

                break;
        }
    }
}

//public class EnemySpawner : MonoBehaviour
//{
//    public List<GameObject> enemyPrefabs; // List of enemy prefabs to spawn
//    float spawnTime = 0f;

//    private void Start()
//    {
//        ObjPool_Enemy.instance.InitPool(enemyPrefabs[0], ObjPool_Enemy.instance.enemyParent, 50);
//    }

//    void Update()
//    {
//        spawnTime += Time.deltaTime;
//        if(spawnTime >= 5f)
//        {
//            spawnTime = 0f;
//            SpawnByTime();
//        }
//    }

//    // Function to spawn enemies based on elapsed game time
//    void SpawnByTime()
//    {
//        int maxEnemies = 0;
//        int enemyCount = 0;

//        // Determine the maximum number of enemies and type based on play time
//        switch (GameManager.instance.playTime)
//        {
//            case < 10:
//                maxEnemies = 2;
//                enemyCount = 0;
//                break;
//            case < 20:
//                maxEnemies = 5;
//                enemyCount = 0;
//                break;
//            case < 30:
//                maxEnemies = 10;
//                enemyCount = 0;
//                break;
//            default:
//                maxEnemies = 15;
//                enemyCount = 0;
//                break;
//        }

//        // Spawn enemies at random positions on a circle
//        for (int i = 0; i < maxEnemies; i++)
//        {
//            Vector2 spawnPosition = RandomOnCircle(Player.instance.transform.position, 10f);
//            SpawnEnemy(spawnPosition, enemyCount);
//        }
//    }

//    // Function to get a random position on the circumference of a circle
//    Vector2 RandomOnCircle(Vector2 center, float r)
//    {
//        float theta = Random.Range(0f, 2f * Mathf.PI);
//        return center + new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * r;
//    }
//    // Function to spawn an enemy at a given position
//    public void SpawnEnemy(Vector3 position, int enemyCount)
//    {
//        if (enemyPrefabs == null || enemyPrefabs.Count == 0)
//        {
//            Debug.LogWarning("Enemy prefabs list is empty or not assigned.");
//            return;
//        }

//        GameObject enemy = ObjPool_Enemy.instance.GetPooledObject();
//        if (enemy != null)
//        {
//            Debug.Log(enemy.name + " is Reusing enemy from ObjPool_Enemy.");
//            enemy.transform.position = position;
//            enemy.SetActive(true);
//            enemy.GetComponent<Enemy>().Initialize();
//        }
//    }
//}
