using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Exp : Item_Drop
{
    public int expAmount;
    float lifeTime;

    public override void Initialize()
    {
        base.Initialize();

        expAmount = 10;
        lifeTime = 10f; // 3 minutes (test 10s)
    }

    public override void Use()
    {
        GameManager.AddExp(expAmount);
    }
    new protected void Update()
    {
        // Collecting and moving to player
        base.Update();

        // Lifetime countdown
        //lifeTime -= Time.deltaTime;
        //if (lifeTime <= 0f)
        //    ObjPool_ItemExp.instance.ReturnToPool(gameObject);
    }
}
