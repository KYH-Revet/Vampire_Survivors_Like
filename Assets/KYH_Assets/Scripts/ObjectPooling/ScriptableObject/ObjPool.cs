using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Pool;

// ScriptableObject for Object Pool
[CreateAssetMenu(fileName = "ObjPool", menuName = "ScriptableObjects/ObjPool", order = 1)]
public class ObjPool : ScriptableObject, IObjPooling
{
    [Header("Object Pool Settings")]
    [SerializeField] GameObject prefab;
    public GameObject _prefab { get { return prefab; } }
    public int poolStartSize;
    [SerializeField] int poolSizeMax;

    Queue<GameObject> pool;

    public void InitPool(Transform parent)
    {
        // Exception Handling
        if (prefab == null)
        {
            Debug.LogWarning("Prefab is null. Cannot initialize pool.");
            return;
        }

        // Clear existing pool if it exists
        if (pool != null)
            ClearPool();

        // Initialize Pool
        pool = new Queue<GameObject>();
        for (int i = 0; i < poolStartSize; i++)
        {
            GameObject obj = Instantiate(prefab, parent);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    /// <summary>
    /// Retrieves an object from the pool.
    /// </summary>
    /// <remarks>If the pool is empty, a warning is logged, and the method returns <see langword="null"/>.
    /// Ensure the pool is properly initialized and has sufficient capacity to avoid empty states.</remarks>
    /// <returns>The next available <see cref="GameObject"/> from the pool, or <see langword="null"/> if the pool is empty.</returns>

    public GameObject GetPooledObject()
    {
        if (pool.Count == 0)
        {
            Debug.LogWarning("Pool is empty. Cannot retrieve object.");
            return null;
        }
        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }
    public void ReturnToPool(GameObject obj)
    {
        if (obj != prefab)
        {
            Debug.LogWarning("Returned object does not match the pool's prefab.");
            return;
        }

        // Only return to pool if it hasn't exceeded the maximum pool size
        if (pool.Count <= poolSizeMax)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
        else
            Destroy(obj);
    }
    public void ClearPool()
    {
        while (pool.Count > 0)
            Destroy(pool.Dequeue());
        pool.Clear();
    }
}
