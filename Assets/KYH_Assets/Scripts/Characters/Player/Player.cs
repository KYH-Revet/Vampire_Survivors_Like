using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    Animator animator;
    public GameObject model;

    [Header("Stats")]
    CharacterStats stats;
    public int health = 100;
    public int speed = 5;

    [Header("UI")]
    public Slider hpBar;
    public TextMeshProUGUI hpText;

    void Awake()
    {
        // Singleton
        Instance();

        // Initialize stats
        stats = new CharacterStats(health, 0, speed, 0);

        // Get Animator component from model
        if (model != null)
            animator = model.GetComponent<Animator>();
        if(animator == null)
            Debug.LogError("Animator component not found on the model.");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize HP Bar
        UIManager.UpdateHpBar(hpBar, hpText, stats.health, stats.maxHealth);
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

        // Animation
        if (dir.magnitude > 0)
        {
            int anim_Direction = animator.GetInteger("Direction");
            if (dir.x < 0)
                anim_Direction = 3; // Left
            else if (dir.x > 0)
                anim_Direction = 2; // Right
            else if (dir.y > 0)
                anim_Direction = 1; // Up
            else if (dir.y < 0)
                anim_Direction = 0; // Down
            else
                return;             // No change
            animator.SetInteger("Direction", anim_Direction);
        }
    }
    public override void Damaged(int dmg)
    {
        int left = stats.health - dmg;
        stats.health = left >= 0 ? left: 0;

        // Update HP Bar
        UIManager.UpdateHpBar(hpBar, hpText, stats.health, stats.maxHealth);

        // Game Over
        if (stats.health == 0)
            GameManager.ChangeState(GameManager.GameState.GameOver);
    }
    protected override void Dead()
    {
        // Game Over
        GameManager.ChangeState(GameManager.GameState.GameOver);
        Debug.Log("Game Over");
    }
}
