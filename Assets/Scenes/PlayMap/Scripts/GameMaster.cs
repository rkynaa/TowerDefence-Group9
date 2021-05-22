using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    private void Start()
    {
        instance = this;
    }

    /// <summary>
    /// Multiplier for tower and upgrade cost
    /// </summary>
    public double costDifficulty = 1;

    /// <summary>
    /// Difficulty multiplier for enemy health, and damage
    /// </summary>
    public double enemyDifficulty = 1;

    public int EnemiesAlive = 0;

    private int money = 1000;

    public Statistics stats = new Statistics();

    [Header("Settings")]
    public static float volume = 1;
    public static bool autoNextRound = true;

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
        return HasMoney((int) value);
    }

    public bool SpendMoney(int value)
    {
        if(money - value < 0 || value < 0)
        {
            return false;
        }
        money -= value;
        return true;
    }

    public bool SpendMoney(double value)
    {
        return SpendMoney((int) value);
    }

    public void GainMoney(int value)
    {
        money += value;
    }
}
