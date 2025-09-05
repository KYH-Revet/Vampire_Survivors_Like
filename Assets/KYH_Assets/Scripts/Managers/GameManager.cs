using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        GameOver
    }
    public static GameState currentState;

    public float playTime = 0f;

    public static void ChangeState(GameState newState)
    {
        currentState = newState;
        // Additional logic for state change can be added here
    }
    
    private void Awake()
    {
        // Singleton
        Instance();

        currentState = GameState.Menu;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playTime += Time.deltaTime;
        UIManager.instance.UpdatePlayTime(playTime);
    }
}
