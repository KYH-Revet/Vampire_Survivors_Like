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

    public Stage_EnemyPools enemyPool;
    [SerializeField] Transform enemyParent;

    void Awake()
    {
        // Singleton
        Instance();

        // Initialize Enemy Pools
        enemyPool.InitializeItemPools(enemyParent);
    }
}