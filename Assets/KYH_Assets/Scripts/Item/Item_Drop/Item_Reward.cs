using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Reward : Item_Drop
{
    public override void Use()
    {
        // Get rewards through GameManager
        Debug.Log("Item_Reward.Use(), not implemented yet");
    }

    new protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(CompareTag("Player"))
        {
            Use();
            Destroy(gameObject);
        }
    }
}
