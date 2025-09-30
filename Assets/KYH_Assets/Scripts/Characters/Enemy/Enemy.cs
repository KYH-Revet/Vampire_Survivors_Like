using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Enemy : Character, IPoolSubscriber
{
    [Header("Enemy Stats")]
    [SerializeField]
    private CharacterStats stats;
    // Base Stats
    public int maxHealth = 50;
    public int speed = 3;
    public int damage = 5;
    public float attackRange = 1.5f;

    [Header("Timers")]
    // Time when the enemy was spawned
    public float bornTime;
    public float destroyTimer = 20f;
    public float lastAttackTime;
    public float attackInterval = 0.5f;

    [Header("Drop Item")]
    public List<GameObject> dropItemPrefab;
    public float dropChance = 0.5f; // 50% chance to drop an item
    public GameObject item_Exp;

    // Enemy State Machine
    public enum State
    {
        live,
        dead
    }
    [Header("State Machine")]
    public State enemyState;
    private void StateMachine()
    {
        if(GameManager.gameState != GameManager.GameState.Playing)
            return;

        switch (enemyState)
        {
            case State.live:
                Move();

                // Auto destroy after 10 seconds (For test)
                if (GameManager.instance.playTime - bornTime > 10f)
                {
                    enemyState = State.dead;
                    GetComponent<Animator>().SetTrigger("Dead");
                }

                if (lastAttackTime <= attackInterval)
                    lastAttackTime += Time.deltaTime;
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
        stats = new CharacterStats(hp, 0, speed, damage);

        bornTime = GameManager.instance.playTime;
        lastAttackTime = attackInterval;

        enemyState = State.live;
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
        if (dir.x > 0 && sr.flipX || dir.x < 0 && !sr.flipX)
            sr.flipX = !sr.flipX;

        if (Vector2.Distance(transform.position, Player.instance.transform.position) > attackRange)
            transform.Translate(dir * stats.speed * Time.deltaTime);
    }
    protected override void Dead()
    {
        // Drop Exp and Item
        GameObject exp = ObjPool_ItemExp.instance.GetPooledObject();
        exp.transform.position = transform.position;

        // Return to object pool
        _pool.ReturnToPool(gameObject);
    }
    public override void HpControl(int value, HpChangeType hpChangeType)
    {
        if (enemyState == State.live)
        {
            switch (hpChangeType)
            {
                case HpChangeType.Heal:
                    stats.health = Mathf.Clamp(stats.health + value, 0, stats.maxHealth);
                    break;
                case HpChangeType.Damage:
                    stats.health = Mathf.Clamp(stats.health - value, 0, stats.maxHealth);

                    // Dead Check
                    if (stats.health <= 0)
                    {
                        enemyState = State.dead;
                        GetComponent<Animator>().SetTrigger("Dead");
                    }
                    break;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemyState == State.live)
        {
            // Attack Cooldown Timer
            if (lastAttackTime >= attackInterval)
            {
                collision.GetComponent<Player>().HpControl(stats.damage, HpChangeType.Damage);
                lastAttackTime = 0f;
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lastAttackTime = 0f;
        }
    }


    // IObserver Implementation for Object Pooling
    IDisposable _subscription;
    ObjPool _pool;
    public void SetSubscription(IDisposable subscription)
    {
        _subscription?.Dispose();
        _subscription = subscription;
    }
    public void SetPool(ObjPool pool)
    {
        _pool = pool;
    }
    public void OnCompleted()
    {
        _pool.ReturnToPool(gameObject);
        _subscription.Dispose();
    }
    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }
    public void OnNext(ObjPool value)
    {
        throw new NotImplementedException();
    }
}
