using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Exp : Item_Drop
{
    // Exp amount
    public int expAmount;

    public override void Initialize()
    {
        base.Initialize();

        // Exp amount
        expAmount = 10;
    }

    public override void Use()
    {
        GameManager.AddExp(expAmount);
    }
}
