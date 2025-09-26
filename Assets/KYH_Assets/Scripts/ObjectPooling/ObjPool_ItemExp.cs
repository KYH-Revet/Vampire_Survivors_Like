using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool_ItemExp : MonoBehaviour, IObjPooling
{
    // Singleton Pattern
    public static ObjPool_ItemExp instance;
    void Instance()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [Header("Exp Obj")]
    public GameObject obj_Exp;
    public int expPoolSize = 30;
    private int expPoolSizeMax = 100;

    [Header("Object Pools")]
    [SerializeField]
    Queue<GameObject> expPool;
    public Transform expParent;

    void Awake()
    {
        // Singleton Pattern
        Instance();

        // Initialize Pools
        expPool = new Queue<GameObject>();
        InitPool(obj_Exp, expParent,  expPoolSize);
    }

    public void InitPool(Transform parent) { }
    public void InitPool(GameObject prefab, Transform parent, int poolSize)
    {
        if(prefab == null)
        {
            Debug.LogWarning("Prefab is null. Cannot initialize pool.");
            return;
        }
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, parent);
            obj.SetActive(false);
            expPool.Enqueue(obj);
        }
    }
    public GameObject GetPooledObject()
    {
        GameObject obj;
        if (expPool.Count > 0)
        {
            obj = expPool.Dequeue();
            obj.GetComponent<Item>().Initialize();
            obj.SetActive(true);
        }
        else
            obj = Instantiate(obj_Exp, expParent);
        return obj;
    }
    public void ReturnToPool(GameObject obj)
    {
        if (expParent.childCount <= expPoolSizeMax)
        {
            obj.SetActive(false);
            expPool.Enqueue(obj);
        }
        else
            Destroy(obj);
    }
    public void ClearPool()
    {
        foreach(GameObject obj in expPool)
            Destroy(obj);
        expPool.Clear();
    }
}
