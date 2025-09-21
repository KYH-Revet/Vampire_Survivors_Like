using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool_Enemy : MonoBehaviour, IObjPooling
{
    // Singleton Pattern
    public static ObjPool_Enemy instance;
    void Instance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [Header("Enemy Obj")]
    GameObject enemyPrefab;
    public int expPoolSize = 30;
    private int expPoolSizeMax = 100;

    [Header("Enemy Pool")]
    [SerializeField]
    List<List<GameObject>> enemyPool;
    public Transform enemyParent;
    public int poolIdx = 0;
    
    void Awake()
    {
        // Singleton Pattern
        Instance();

        // Initialize Pools
        enemyPool = new List<List<GameObject>>() { new List<GameObject>(), new List<GameObject>() };
    }

    public void InitPool(GameObject prefab, Transform parent, int poolSize)
    {
        if (prefab == null)
        {
            Debug.LogWarning("Prefab is null. Cannot initialize pool.");
            return;
        }
        enemyPrefab = prefab;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.parent = parent;
            obj.SetActive(false);
            enemyPool[poolIdx].Add(obj);
        }
    }
    public GameObject GetPooledObject()
    {
        GameObject obj = null;
        foreach(GameObject enemy in enemyPool[poolIdx])
        {
            if(enemy.activeInHierarchy == false)
                return enemy;
        }
        obj = Instantiate(enemyPool[poolIdx][0]);
        return obj;
    }
    public void ReturnToPool(GameObject obj)
    {
        if (enemyPool[poolIdx].Count <= expPoolSizeMax)
            obj.SetActive(false);
        else
        {
            enemyPool[poolIdx].Remove(obj);
            Destroy(obj);
        }
    }
    public void ClearPool()
    {
        foreach (GameObject obj in enemyPool[poolIdx])
            Destroy(obj);
        enemyPool[poolIdx].Clear();
    }
}
