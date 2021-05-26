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
        AddUpgrade(new UpgradeDamage());
        AddUpgrade(new UpgradePierce());
        AddUpgrade(new UpgradeTracking());
    }

    // Change this if you need multiple projectiles or other.
    public override void Attack()
    {
        base.Attack();
        // target.DamageEntity(5);
    }

    private class UpgradeDamage : Upgrade
    {
        readonly int[] cost = new int[] { 60, 100, 200, 300, 500 };

        public UpgradeDamage()
        {
            maxLevel = cost.Length;
        }

        protected override int CalcCost()
        {
            return cost[level];
        }

        public override string GetName()
        {
            return "Damage";
        }

        public override void OnUpgrade()
        {
            tower.attackProjectile.damage += 5;
        }
    }

    private class UpgradeSpeed : Upgrade
    {
        readonly int[] cost = new int[] { 60, 100, 200, 300, 500 };

        public UpgradeSpeed()
        {
            maxLevel = cost.Length;
        }

        protected override int CalcCost()
        {
            return cost[level];
        }

        public override string GetName()
        {
            return "Attack Speed";
        }

        public override void OnUpgrade()
        {
            tower.attackSpeed += 0.4f;
        }
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

    private class UpgradeTracking : Upgrade
    {
        protected override int CalcCost()
        {
            return 500;
        }

        public override string GetName()
        {
            return "Tracking";
        }

        public override void OnUpgrade()
        {
            ((TrackingProjectile)tower.attackProjectile).enableTracking = true;
        }
    }

}
