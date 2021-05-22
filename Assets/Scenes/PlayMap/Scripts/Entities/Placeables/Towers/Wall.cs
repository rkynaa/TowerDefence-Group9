using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : TowerEntity
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Initialise upgrades here
        AddUpgrade(new UpgradeHealth());
    }

    // Wall cannot attack
    public override void Attack()
    {

    }

    private class UpgradeHealth : Upgrade
    {
        readonly int[] cost = new int[5] { 50, 60, 70, 90, 150 };

        public UpgradeHealth()
        {
            maxLevel = cost.Length;
        }

        protected override int CalcCost()
        {
            return cost[level];
        }

        public override string GetName()
        {
            return "Health";
        }

        public override void OnUpgrade()
        {
            tower.Health += 5;
            tower.MaxHealth += 5;
        }
    }
}
