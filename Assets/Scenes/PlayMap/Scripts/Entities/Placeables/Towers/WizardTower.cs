using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTower : TowerEntity
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Initialise upgrades here
        AddUpgrade(new UpgradePierce());
    }

    // Change this if you need multiple projectiles or other.
    public override void Attack()
    {
        base.Attack();
        // target.DamageEntity(5);
    }

    private class UpgradePierce : Upgrade
    {
        readonly int[] cost = new int[] { 150, 250 };

        public UpgradePierce()
        {
            maxLevel = cost.Length;
        }

        protected override int CalcCost()
        {
            return cost[level];
        }

        public override string GetName()
        {
            return "Pierce";
        }

        public override void OnUpgrade()
        {
            tower.attackProjectile.pierce += 1;
        }
    }


}
