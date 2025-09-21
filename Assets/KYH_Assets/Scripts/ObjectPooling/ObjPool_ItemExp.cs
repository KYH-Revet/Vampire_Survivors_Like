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
    public List<GameObject> expPool;
    public Transform expParent;

    void Awake()
    {
        // Singleton Pattern
        Instance();

        // Initialize Pools
        expPool = new List<GameObject>();
        InitPool(obj_Exp, expParent,  expPoolSize);
    }

    public void InitPool(GameObject prefab, Transform parent, int poolSize)
    {
        if(prefab == null)
        {
            Debug.LogWarning("Prefab is null. Cannot initialize pool.");
            return;
        }
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.parent = parent;
            obj.SetActive(false);
            expPool.Add(obj);
        }
    }
    public GameObject GetPooledObject()
    {
        foreach(GameObject obj in expPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.GetComponent<Item>().Initialize();
                return obj;
            }
        }
        GameObject exp = Instantiate(obj_Exp);
        exp.transform.parent = expParent;
        expPool.Add(exp);
        return exp;
    }
    public void ReturnToPool(GameObject obj)
    {
        if (expPool.Count <= expPoolSizeMax)
            obj.SetActive(false);
        else
        {
            Debug.Log(obj.name + " is Destroyed from pool.");
            expPool.Remove(obj);
            Destroy(obj);
        }
    }
    public void ClearPool()
    {
        foreach(GameObject obj in expPool)
            Destroy(obj);
        expPool.Clear();
    }
}
