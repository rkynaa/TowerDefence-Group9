using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsDisplay : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Text text = GetComponent<Text>();
        Statistics s = GameMaster.instance.stats;
        text.text = "Enemies Killed: " + s.enemiesKilled + "\n" +
            "Bosses Killed: " + s.bossesKilled + "\n" +
            "Towers Placed: " + s.towersPlaced + "\n" +
            "Towers Lost: " + s.towersLost + "\n" +
            "Towers Cost: " + s.towersCost + "\n" +
            "Repair Cost: " + s.repairCost + "\n" +
            "Upgrades Applied: " + s.upgradesApplied + "\n" +
            "Upgrades Cost: " + s.upgradesCost + "\n" +
            "Damage Dealt: " + s.damageDealt + "\n" +
            "Damage Taken: " + s.damageTaken + "\n" +
            "Money Generated: " + s.moneyGenerated + "\n";

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
