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
    
    // Item Pool
    public List<ObjPool> itemPools;
    public Transform itemParent;

    // Unity Functions
    void Awake()
    {
        // Singleton
        Instance();

        gameState = GameState.Playing;
    }
    void Update()
    {
        PlayTimeUpdate();
    }

    // Play time update
    void PlayTimeUpdate()
    {
        // Only update play time when the game is in the Playing state
        if (gameState != GameState.Playing)
            return;

        playTime += Time.deltaTime;

        // Update UI
        UIManager.instance.UpdatePlayTime(playTime);
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
        }
    }
}
