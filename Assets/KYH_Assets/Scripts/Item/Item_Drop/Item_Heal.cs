using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Heal : Item_Drop
{
    public int healAmount;
    public override void Initialize()
    {
        base.Initialize();

        // heal amount
        healAmount = 10;
    }

    public override void Use()
    {
        Player.instance.HpControl(healAmount, Character.HpChangeType.Heal);
    }
}
