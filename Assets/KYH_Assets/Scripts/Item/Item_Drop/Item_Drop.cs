using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Drop : Item
{
    protected bool isCollected;
    protected int minSpeed;
    protected int maxSpeed;
    public override void Initialize()
    {
        itemType = ItemType.Drop;
        isCollected = false;
        minSpeed = 5;
        maxSpeed = 20;
    }    
    public override void Use()
    {
        throw new System.NotImplementedException();
    }

    protected void Start()
    {
        Initialize();
    }
    protected void Update()
    {
        if (isCollected)
            MoveToPlayer();
    }
    protected void MoveToPlayer()
    {
        if (Player.instance == null || GameManager.gameState == GameManager.GameState.Playing)
            return;
        Vector2 dir = (Player.instance.transform.position - transform.position).normalized;
        float speed = Mathf.Lerp(minSpeed, maxSpeed, 0.5f);
        transform.Translate(dir * speed * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Area_DropItem":
                isCollected = true;
                break;
            case "Player":
                Use();
                Destroy(gameObject);
                break;
        }
    }
}
