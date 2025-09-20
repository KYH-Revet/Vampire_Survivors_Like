using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjPooling
{
    void InitPool(GameObject prefab, Transform parent, int poolSize);
    GameObject GetPooledObject();
    void ReturnToPool(GameObject obj);
    void ClearPool();
}
