using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy : Character
{
    // Enemy Stats
    public struct EnemyStats
    {
        public int health;
        public int speed;
        public int damage;
        public EnemyStats(int health, int speed, int damage)
        {
            this.health = health;
            this.speed = speed;
            this.damage = damage;
        }
    }
    public EnemyStats stats;
    // Base Stats
    public int maxHealth = 50;
    public int speed = 3;

    // Time when the enemy was spawned
    public float bornTime;
    public float destroyTimer = 20f;

    // Enemy State Machine
    public enum State
    {
        live,
        dead
    }
    public State currentState;
    private void StateMachine()
    {
        switch (currentState)
        {
            case State.live:
                Move();

                // Auto destroy after 10 seconds (For test)
                if (GameManager.instance.playTime - bornTime > 10f)
                {
                    currentState = State.dead;
                    GetComponent<Animator>().SetTrigger("Dead");
                }
                break;
            case State.dead:
                destroyTimer -= Time.deltaTime;
                if (destroyTimer <= 0)
                    Destroy(gameObject);
                break;
        }
    }


    // Initialize enemy stats based on game time
    public void Initialize()
    {
        int hp = GameManager.instance.playTime == 0 ? maxHealth : maxHealth + (int)(GameManager.instance.playTime / 60) * 10;
        stats = new EnemyStats(hp, speed, 10);

        bornTime = GameManager.instance.playTime;
        destroyTimer = 20f;

        currentState = State.live;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
    }

    protected override void Move()
    {
        Vector2 dir = (Player.instance.transform.position - transform.position).normalized;

        // Sprite Flip
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(dir.x > 0 && !sr.flipX || dir.x < 0 && sr.flipX)
            sr.flipX = !sr.flipX;

        if (Vector2.Distance(transform.position, Player.instance.transform.position) > 0.2f)
            transform.Translate(dir * stats.speed * Time.deltaTime);
    }
    protected override void Dead()
    {
        gameObject.SetActive(false);
    }
}
