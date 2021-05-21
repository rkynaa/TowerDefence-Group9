using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    /// <summary>
    /// Multiplier for tower and upgrade cost
    /// </summary>
    public static double costDifficulty = 1;

    /// <summary>
    /// Difficulty multiplier for enemy health, and damage
    /// </summary>
    public static double enemyDifficulty = 1;

    public static int EnemiesAlive = 0;

    private static int money = 0;

    public static int GetMoney()
    {
        return money;
    }

    public static bool HasMoney(int value)
    {
        if (money - value < 0)
        {
            return false;
        }
        return true;
    }

    public static bool HasMoney(double value)
    {
        return HasMoney((int) value);
    }

    public static bool SpendMoney(int value)
    {
        if(money - value < 0)
        {
            return false;
        }
        money -= value;
        return true;
    }

    public static bool SpendMoney(double value)
    {
        return SpendMoney((int) value);
    }
}
