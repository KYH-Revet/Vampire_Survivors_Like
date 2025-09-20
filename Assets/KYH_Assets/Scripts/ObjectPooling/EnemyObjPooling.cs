using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjPooling : MonoBehaviour, IObjPooling
{
    // Singleton Pattern
    public static EnemyObjPooling instance;
    void Instance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    List<GameObject> enemyPool;
    GameObject enemyPrefab;
    public int expPoolSize = 30;
    public int expPoolSizeMax = 100;

    void Awake()
    {
        // Singleton Pattern
        Instance();

        // Initialize Pools
        enemyPool = new List<GameObject>();
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
            enemyPool.Add(obj);
        }
    }
    public GameObject GetPooledObject()
    {
        GameObject obj = null;
        // 적 종류에 따라 다르게 처리할 방법 찾아야함
        foreach(GameObject enemy in enemyPool)
        {
            if(enemy.activeInHierarchy == false)
                return enemy;
        }
        obj = Instantiate(enemyPool[0]);
        return obj;
    }
    public void ReturnToPool(GameObject obj)
    {
        if (enemyPool.Count <= expPoolSizeMax)
            obj.SetActive(false);
        else
        {
            Debug.Log(obj.name + " is Destroyed from pool.");
            enemyPool.Remove(obj);
            Destroy(obj);
        }
    }
    public void ClearPool()
    {
        foreach (GameObject obj in enemyPool)
            Destroy(obj);
        enemyPool.Clear();
    }
}
