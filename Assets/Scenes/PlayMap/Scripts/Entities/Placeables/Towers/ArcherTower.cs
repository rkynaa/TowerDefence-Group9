using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : TowerEntity
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Initialise upgrades here
        AddUpgrade(new UpgradeDamage());
        AddUpgrade(new UpgradeSpeed());
        AddUpgrade(new UpgradeRange());
        AddUpgrade(new UpgradeDoubleShot());
    }

    // Change this if you need multiple projectiles or other.
    public override void Attack()
    {
        base.Attack();
        // target.DamageEntity(5);
    }

    private class UpgradeDamage : Upgrade
    {
        readonly int[] cost = new int[5] { 50, 60, 70, 90, 150 };

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
        readonly int[] cost = new int[5] { 45, 65, 70, 90, 150 };

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
            tower.attackSpeed += 0.1f;
        }
    }

    private class UpgradeRange : Upgrade
    {
        readonly int cost = 150;

        protected override int CalcCost()
        {
            return cost;
        }

        public override string GetName()
        {
            return "Range";
        }

        public override void OnUpgrade()
        {
            tower.Range += 1f;
        }
    }

    private class UpgradeDoubleShot : Upgrade
    {
        readonly int cost = 200;

        protected override int CalcCost()
        {
            return cost;
        }

        public override string GetName()
        {
            return "Double Shot";
        }

        public override void OnAttack()
        {
            tower.StartCoroutine(Attack());
        }

        IEnumerator Attack()
        {
            yield return new WaitForSeconds(0.1f);
            tower.Attack();
        }
}
}
