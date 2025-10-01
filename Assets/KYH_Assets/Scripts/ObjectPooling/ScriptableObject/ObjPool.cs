using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Pool;

// ScriptableObject for Object Pool
[CreateAssetMenu(fileName = "ObjPool", menuName = "ScriptableObjects/ObjPool", order = 1)]
/*
public class ObjPool : ScriptableObject, IObjPooling, IObservable<ObjPool>
{
    [Header("Object Pool Settings")]
    [SerializeField] GameObject prefab;
    public GameObject _prefab { get { return prefab; } }
    [SerializeField] int poolStartSize;
    [SerializeField] int poolSizeMax;

    Queue<GameObject> pool;

    // Interface Implementation
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
            CreateInstance(parent);

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
        foreach (var observer in observers)
            observer.OnCompleted();

        while (pool.Count > 0)
            Destroy(pool.Dequeue());
        pool.Clear();
    }
    public void SlowInitPool(Transform parent, float time)
    {
        // Clear existing pool if it exists
        if (pool != null)
            ClearPool();
        // Initialize Pool
        SlowInitCoroutine(parent, time);
    }

    // Private Helper Methods
    void CreateInstance(Transform parent)
    {
        // Instantiate and add to pool
        GameObject obj = Instantiate(prefab, parent);
        obj.SetActive(false);
        pool.Enqueue(obj);

        // Set up observer pattern
        IPoolSubscriber sub = obj.GetComponent<IPoolSubscriber>();
        sub.SetPool(this);
        sub.SetSubscription(Subscribe(sub));
    }
    IEnumerator SlowInitCoroutine(Transform parent, float cycleTime)
    {
        while (pool.Count < poolStartSize)
        {
            CreateInstance(parent);
            yield return new WaitForSeconds(cycleTime);
        }
    }

    // IObservable Implementation
    List<IObserver<ObjPool>> observers = new List<IObserver<ObjPool>>();
    public IDisposable Subscribe(IObserver<ObjPool> observer)
    {
        observers.Add(observer);
        return new Unsubscriber<ObjPool>(observers, observer);        
    }
}
*/

public class ObjPool : ScriptableObject, IObjectPool<GameObject>, IObservable<ObjPool>
{
    [Header("Object Pool Settings")]
    [SerializeField] GameObject prefab;
    public GameObject _prefab { get { return prefab; } }
    Transform parent;

    [SerializeField] int poolStartSize;
    [SerializeField] int poolSizeMax;

    ObjectPool<GameObject> pool;

    public void InitPool()
    {
        pool = new ObjectPool<GameObject>(
            () =>
            {
                // Instantiate and add to pool
                GameObject obj = Instantiate(prefab, parent);
                obj.SetActive(false);

                // Set up observer pattern
                IPoolSubscriber sub = obj.GetComponent<IPoolSubscriber>();
                sub.SetPool(this);
                sub.SetSubscription(Subscribe(sub));
                return obj;
            },                                  // Create
            obj => { obj.SetActive(true); },    // Get
            obj => { obj.SetActive(false); },   // Release
            obj => { Destroy(obj); },           // Destroy
            true, poolStartSize, poolSizeMax);  // Collection Check, Default Capacity, Max Size

        Debug.Log($"Initialized pool with {poolStartSize} objects of {prefab.name}");
    }
    public void InitPool(Transform parent)
    {
        this.parent = parent;
        InitPool();
    }

    // IObjectPool Implementation
    public int CountInactive => pool.CountAll - pool.CountActive;
    public GameObject Get()
    {
        return pool.Get();
    }
    public PooledObject<GameObject> Get(out GameObject v)
    {
        v = pool.Get();
        return new PooledObject<GameObject>();
    }
    public void Release(GameObject obj)
    {
        if(CountInactive >= poolSizeMax)
            Destroy(obj);
        else
            pool.Release(obj);
    }
    public void Clear()
    {
        // Notify observers before clearing
        foreach (var observer in observers)
            observer.OnCompleted();

        // Clear the pool
        pool.Dispose();
    }

    // IObservable Implementation
    List<IObserver<ObjPool>> observers = new List<IObserver<ObjPool>>();
    public IDisposable Subscribe(IObserver<ObjPool> observer)
    {
        observers.Add(observer);
        return new Unsubscriber<ObjPool>(observers, observer);
    }
}
