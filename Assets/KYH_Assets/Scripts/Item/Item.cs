using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public enum ItemType
    {
        Weapon,
        Support,
        Drop
    }
    public ItemType itemType;
    public abstract void Initialize();
    public abstract void Use();
}
