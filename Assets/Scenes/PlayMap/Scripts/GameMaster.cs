using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
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

    public static bool SpendMoney(int value)
    {
        if(money - value < 0)
        {
            return false;
        }
        money -= value;
        return true;
    }



}
