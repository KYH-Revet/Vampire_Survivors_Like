using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage_EnemyPools", menuName = "ScriptableObjects/Stage/Stage_EnemyPools", order = 1)]
public class Stage_EnemyPools : ScriptableObject
{
    [Header("Enemy Pools")]
    public List<ObjPool> enemyPools;
    
    public void InitializeItemPools(Transform parent)
    {
        foreach (var pool in enemyPools)
            pool.InitPool(parent);
    }
}