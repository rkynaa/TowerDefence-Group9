using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTower : TowerEntity
{
    private float cooldown = 0;
    private float gainInterval = 10;
    private int gainAmount = 20;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        cooldown = gainInterval;

        // Initialise upgrades here
        AddUpgrade(new UpgradeGain());
        AddUpgrade(new UpgradeInterval());
    }

    protected override void Update()
    {
        base.Update();

        if (GameMaster.instance.enemiesAlive.Count > 0)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0)
            {
                cooldown = gainInterval;
                GameMaster.instance.stats.moneyGenerated += gainAmount;
                GameMaster.instance.GainMoney(gainAmount);
            }
        }
    }

    private class UpgradeGain : Upgrade
    {
        readonly int[] cost = new int[5] { 50, 60, 70, 90, 150 };

        public UpgradeGain()
        {
            maxLevel = cost.Length;
        }

        protected override int CalcCost()
        {
            return cost[level];
        }

        public override string GetName()
        {
            return "Increase Gain";
        }

        public override void OnUpgrade()
        {
            ((GoldTower)tower).gainAmount += 5;
        }
    }

    private class UpgradeInterval : Upgrade
    {
        readonly int[] cost = new int[4] { 60, 65, 80, 150 };

        public UpgradeInterval()
        {
            maxLevel = cost.Length;
        }

        protected override int CalcCost()
        {
            return cost[level];
        }

        public override string GetName()
        {
            return "Speed";
        }

        public override void OnUpgrade()
        {
            ((GoldTower)tower).gainInterval -= 0.5f;
        }
    }
}
