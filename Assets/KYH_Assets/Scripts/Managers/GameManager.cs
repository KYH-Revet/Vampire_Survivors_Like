using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance;
    private void Instance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Game States
    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        GameOver
    }
    public static GameState gameState;
    
    // Level, Exp
    public static int level = 1;
    public static int maxExp = 100;
    public static int exp = 0;

    // Timers
    public float playTime = 0f;    
    public float testLevelUpTimer = 0f; // For testing level up

    // Unity Functions
    private void Awake()
    {
        // Singleton
        Instance();
    }
    void Start()
    {
        gameState = GameState.Playing;
    }
    void Update()
    {
        playTime += Time.deltaTime;
        UIManager.instance.UpdatePlayTime(playTime);

        
        testLevelUpTimer += Time.deltaTime;
        if(testLevelUpTimer >= 5f) // Every 5 seconds
        {
            testLevelUpTimer = 0f;

            // Exp test
            AddExp(10);
        }
    }

    // Change Game State
    public static void ChangeState(GameState newState)
    {
        gameState = newState;
        // Additional logic for state change can be added here
    }

    // Add Experience Points
    public static void AddExp(int amount)
    {
        exp += amount;
        
        if(exp >= maxExp) // Example leveling logic
        {
            exp -= maxExp;
            maxExp += ++level * 10;
            Player player = Player.instance;
            player.HpControl(player.playerStats.maxHealth / 10, Character.HpChangeType.Heal);
            Debug.Log("Leveled up to " + level);
        }
    }
}
