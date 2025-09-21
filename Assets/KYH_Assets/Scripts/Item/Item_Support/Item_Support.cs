using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Support : Item
{
    public override void Initialize()
    {
        itemType = ItemType.Support;
    }
    public override void Use()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        if(GameManager.gameState == GameManager.GameState.Playing)
            Use();
    }
}
