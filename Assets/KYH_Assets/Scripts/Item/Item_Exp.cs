using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Exp : Item
{
    // Exp amount
    public int expAmount = 10;

    // Move to player
    bool isCollected = false;
    int minSpeed = 5;
    int maxSpeed = 20;

    private void Update()
    {
        if(isCollected)
            MoveToPlayer();
    }

    override public void Use()
    {
        GameManager.AddExp(expAmount);
    }

    
    void MoveToPlayer()
    {
        if (Player.instance == null)
            return;
        Vector2 dir = (Player.instance.transform.position - transform.position).normalized;
        float speed = Mathf.Lerp(minSpeed, maxSpeed, 0.5f);
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Area_Exp":
                isCollected = true;
                break;
            case "Player":
                Use();
                Destroy(gameObject);
                break;
        }
    }
}
