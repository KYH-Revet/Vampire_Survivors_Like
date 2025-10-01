using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // Singleton
    public static Stage instance;
    public void Instance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [Header("Enemy Pools")]
    public Stage_EnemyPools enemyPool;
    [SerializeField] Transform enemyParent;
    int currentPoolIdx = 0;

    void Awake()
    {
        // Singleton
        Instance();

        // Initialize Enemy Pools
        if (enemyPool == null)
            Debug.LogWarning("Enemy Pool is not assigned.");
        else
        {
            Debug.Log("Initializing Enemy Pools...");
            enemyPool.InitializeEnemyPools(enemyParent, currentPoolIdx++);
        }
    }

    void Update()
    {
        if (enemyPool == null)
            Debug.LogWarning("Enemy Pool is not assigned.");
        else
            SpawnTest();
    }

    float spawnTimer = 0f;
    float spawnInterval = 2f;
    int phase = 1;
    Vector2 RandomOnCircle(Vector2 center, float r)
    {
        float theta = Random.Range(0f, 2f * Mathf.PI);
        return center + new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * r;
    }
    void SpawnTest()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnInterval)
        {
            for(int i = 0; i < phase * 2; i++)
            {
                Spawn();
            }
            switch (phase)
            {
                case 1:
                    if (GameManager.instance.playTime > 10f)
                        phase++;
                    break;
                case 2:
                    if (GameManager.instance.playTime > 20f)
                        phase++;
                    break;
                case 3:
                    if (GameManager.instance.playTime > 30f)
                        phase++;
                    break;
                case 4:
                    if (GameManager.instance.playTime > 40f)
                        phase++;
                    break;
                case 5:
                    break;
            }
        }
        
        
    }
    void Spawn()
    {
        spawnTimer = 0f;
        GameObject obj = enemyPool.GetEnemyInPool(currentPoolIdx - 1);
        obj.transform.position = RandomOnCircle(Player.instance.transform.position, 10f);
        obj.GetComponent<Enemy>().Initialize();
    }
}