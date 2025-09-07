using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton
    public static UIManager instance;
    private void Instance()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Play Time Text
    [Header("Texts")]
    [Tooltip("Display play time")]
    public TextMeshProUGUI playTimeText;

    void Awake()
    {
        // Singleton
        Instance();
    }
    public void UpdatePlayTime(float time)
    {
        if (playTimeText == null)
        {
            Debug.LogWarning("PlayTimeText is not assigned in UIManager.");
            return;
        }
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        playTimeText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
