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
    public CharacterStats playerStats;
    public int health = 100;
    public int speed = 5;

    [Header("UI")]
    public Slider[] hpBars;
    public TextMeshProUGUI hpText;

    void Initialize()
    {
        // stats
        playerStats = new CharacterStats(health, 0, speed, 0);

        // Get Animator component from model
        if (model != null)
            animator = model.GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("Animator component not found on the model.");
    }

    void Awake()
    {
        // Singleton
        Instance();

        // Initialize
        Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
        // Initialize HP Bar
        foreach(Slider hpBar in hpBars)
            UIManager.UpdateHpBar(hpBar, hpText, playerStats.health, playerStats.maxHealth);
    }
    // Update is called once per frame
    void Update()
    {
        // Speed 변경 감지 (Test 용)
        if (speed != playerStats.speed)
            playerStats.speed = speed;

        // Value of Player input
        Move();
    }

    protected override void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(h, v).normalized;
        transform.Translate(dir * playerStats.speed * Time.deltaTime);

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
    protected override void Dead()
    {
        // Game Over
        GameManager.ChangeState(GameManager.GameState.GameOver);
        Debug.Log("Game Over");
    }
    public override void HpControl(int value, HpChangeType hpChangeType)
    {
        // Validate input
        if (value < 0)
        {
            Debug.LogWarning("HpControl value should be non-negative.");
            return;
        }

        // Only allow HP change during Playing state
        if (GameManager.gameState == GameManager.GameState.Playing)
        {
            // Change HP
            switch (hpChangeType)
            {
                case HpChangeType.Heal:
                    playerStats.health = Mathf.Clamp(playerStats.health + value, 0, playerStats.maxHealth);
                    break;
                case HpChangeType.Damage:
                    playerStats.health = Mathf.Clamp(playerStats.health - value, 0, playerStats.maxHealth);
                    break;
            }
            // Update HP Bar
            foreach (Slider hpBar in hpBars)
                UIManager.UpdateHpBar(hpBar, hpText, playerStats.health, playerStats.maxHealth);
        }

        // Game Over
        if (playerStats.health <= 0)
            GameManager.ChangeState(GameManager.GameState.GameOver);
    }
}
