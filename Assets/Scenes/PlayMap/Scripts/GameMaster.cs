using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Kushan

public class GameMaster
{
    public static GameMaster instance;

    public static void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Creating GameMaster object.");
            instance = new GameMaster();
        }
        else
        {
            Debug.Log("Detected duplicate GameMaster object.");
        }
    }

    /// <summary>
    /// Multiplier for tower and upgrade cost
    /// </summary>
    public double costDifficulty = 1;

    /// <summary>
    /// Difficulty multiplier for enemy health, and damage
    /// </summary>
    public double enemyDifficulty = 1;

    [HideInInspector]
    public List<EnemyEntity> enemiesAlive;

    private int money = 1000;

    [HideInInspector]
    public Statistics stats = new Statistics();

    [HideInInspector]
    public CoreEntity core;

    public Text moneyText;//k

    [Header("Settings")]
    public static float volume = 1;
    public static bool autoNextRound = true;//k


    void Start()
    {
        money = (int)(money / costDifficulty);
        
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
        moneyText.text = money.ToString();//kushan
        return true;
    }

    public bool SpendMoney(double value)
    {
        return SpendMoney((int)value);
    }

    public void GainMoney(int value)
    {
        money += value;
        moneyText.text = money.ToString();//kushan
    }

    //k
    //public void Easydifficulty()
    //{
        //GameMaster.instance.
      //  costDifficulty = 0.5;
        //GameMaster.instance.
        //enemyDifficulty = 0.5;
    //}

    //k
    //public void Harddifficulty()
    //{
        //GameMaster.instance.
    //    costDifficulty = 4;
        //GameMaster.instance.
    //    enemyDifficulty = 4;
    //}



}
