using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    // Singleton
    public static Player instance;
    private void Instance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    struct PlayerStats
    {
        public int health;
        public int speed;

        public PlayerStats(int health, int speed)
        {
            this.health = health;
            this.speed = speed;
        }
    }
    PlayerStats stats;
    public int speed = 5;

    void Awake()
    {
        // Singleton
        Instance();

        // Initialize stats
        stats = new PlayerStats(100, speed);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Speed 변경 감지 (Test 용)
        if (speed != stats.speed)
            stats.speed = speed;

        // Value of Player input
        Move();
    }

    protected override void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(h, v).normalized;
        transform.Translate(dir * stats.speed * Time.deltaTime);
    }
    protected override void Dead()
    {
        // Game Over
        GameManager.ChangeState(GameManager.GameState.GameOver);
        Debug.Log("Game Over");
    }
}
