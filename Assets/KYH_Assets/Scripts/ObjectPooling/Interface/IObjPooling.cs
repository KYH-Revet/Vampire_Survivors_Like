using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjPooling
{
    void InitPool(Transform parent);
    GameObject GetPooledObject();
    void ReturnToPool(GameObject obj);
    void ClearPool();
}
