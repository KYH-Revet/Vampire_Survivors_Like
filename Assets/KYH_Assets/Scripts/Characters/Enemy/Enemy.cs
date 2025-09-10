using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy : Character
{
    [Header("Enemy Stats")]
    [SerializeField]
    private CharacterStats stats;
    // Base Stats
    public int maxHealth = 50;
    public int speed = 3;
    public int damage = 5;

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
        stats = new CharacterStats(hp, 0, speed, damage);

        bornTime = GameManager.instance.playTime;
        destroyTimer = 20f;
        lastAttackTime = attackInterval*2;

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

        if (lastAttackTime <= attackInterval * 2)
            lastAttackTime += Time.deltaTime;
    }

    protected override void Move()
    {
        Vector2 dir = (Player.instance.transform.position - transform.position).normalized;

        // Sprite Flip
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(dir.x > 0 && sr.flipX || dir.x < 0 && !sr.flipX)
            sr.flipX = !sr.flipX;

        if (Vector2.Distance(transform.position, Player.instance.transform.position) > 0.1f)
            transform.Translate(dir * stats.speed * Time.deltaTime);
    }
    public override void Damaged(int dmg)
    {
        stats.health -= dmg;
        if (stats.health <= 0 && currentState != State.dead)
        {
            currentState = State.dead;
            GetComponent<Animator>().SetTrigger("Dead");
        }
    }
    protected override void Dead()
    {
        // Drop Exp and Item
        Instantiate(item_Exp, transform.position, Quaternion.identity);

        // Return to object pool
        gameObject.SetActive(false);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && currentState == State.live)
        {
            // Attack Cooldown Timer
            if (lastAttackTime >= attackInterval)
            {
                Debug.Log("Enemy attacks Player");
                collision.GetComponent<Player>().Damaged(stats.damage);
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
}
