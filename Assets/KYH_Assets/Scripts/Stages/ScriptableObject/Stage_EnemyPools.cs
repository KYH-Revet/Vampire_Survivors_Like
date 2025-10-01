using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Stage_EnemyPools", menuName = "ScriptableObjects/Stage/Stage_EnemyPools", order = 1)]
public class Stage_EnemyPools : ScriptableObject
{
    [Header("Enemy Pools")]
    public List<ObjPool> enemyPools;
    int poolIdx = 0;
    
    public void InitializeEnemyPools(Transform parent)
    {
        // Exception Handling
        if (poolIdx > enemyPools.Count)
        {
            Debug.LogWarning("All enemy pools have been initialized.");
            return;
        }

        // Initialize the next enemy pool
        enemyPools[poolIdx++].InitPool(parent);
    }
    public GameObject GetEnemyInPool()
    {
        // Exception Handling
        if (poolIdx > enemyPools.Count)
        {
            Debug.LogWarning("All enemy pools have been used.");
            return null;
        }

        // Get an enemy from the current pool
        return enemyPools[poolIdx].Get();
    }
}