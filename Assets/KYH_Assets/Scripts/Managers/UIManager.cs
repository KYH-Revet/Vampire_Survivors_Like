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

    [Header("Player HP Bar")]
    public Slider hpBar;
    public TextMeshProUGUI hpText;

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
        playTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public static void UpdateHpBar(Slider hpBar, TextMeshProUGUI hpText, int currentHP, int maxHP)
    {
        if (hpBar == null || hpText == null)
        {
            Debug.LogWarning("HP Bar or HP Text is not assigned in UIManager.");
            return;
        }
        hpBar.maxValue = maxHP;
        hpBar.value = currentHP >= 0 ? currentHP : 0;
        hpText.text = currentHP + "/" + maxHP;
    }
}
