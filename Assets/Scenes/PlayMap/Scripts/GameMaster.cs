using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Kushan
using TMPro;

public class GameMaster
{
    public static GameMaster instance;

    public static void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Creating GameMaster object.");
            instance = new GameMaster();
            instance.Start();
            green.a = 120;
            red.a = 120;
        }
        else
        {
            Debug.Log("Detected duplicate GameMaster object.");
            if (!instance.started)
            {
                instance.Start();
            }
        }
    }

    public static void Reset()
    {
        Debug.Log("Creating GameMaster object.");
        instance = new GameMaster();
    }

    public bool started = false;

    /// <summary>
    /// Multiplier for tower and upgrade cost
    /// </summary>
    public double costDifficulty = 1.3;

    /// <summary>
    /// Difficulty multiplier for enemy health, and damage
    /// </summary>
    public double enemyDifficulty = 1.1;

    [HideInInspector]
    public List<EnemyEntity> enemiesAlive = new List<EnemyEntity>();

    public int money = 800;

    [HideInInspector]
    public Statistics stats = new Statistics();

    [HideInInspector]
    public CoreEntity core;

    public TextMeshProUGUI moneyText;//k

    [Header("Settings")]
    public static float volume = 1;
    public static bool autoNextRound = true;//k

    public static Color green = Color.green;
    public static Color red = Color.red;


    void Start()
    {
        UpdateMoney();
        started = true;
    }

    public int GetMoney()
    {
        return money;
    }

    public bool HasMoney(int value)
    {
        if (money - value < 0)
        {
            return false;
        }
        return true;
    }

    public bool HasMoney(double value)
    {
        return HasMoney((int)value);
    }

    public bool SpendMoney(int value)
    {
        if (money - value < 0 || value < 0)
        {
            return false;
        }
        money -= value;
        UpdateMoney();
        return true;
    }

    public bool SpendMoney(double value)
    {
        return SpendMoney((int)value);
    }

    public void GainMoney(int value)
    {
        money += value;
        UpdateMoney();
    }

    /// <summary>
    /// Called after money is changed
    /// </summary>
    public void UpdateMoney()
    {
        if (moneyText != null)
        {
            moneyText.text = money.ToString();//kushan
        }
        Shop.UpdateColor();
        TowerPanel.UpdateUI();
    }
}
