using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethower : TowerEntity
{

    [Header("Flamethower")]
    public FlameProjectile flameProj;
    public ParticleSystem flame;
    private ParticleSystem.MainModule main;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        main = flame.main;
        main.duration = (1 / attackSpeed);

        // Initialise upgrades here
        AddUpgrade(new UpgradeDamage());
    }

    protected override void Update()
    {
        base.Update();

        if (target == null && flame.isPlaying)
        {
            flame.Stop();
        }
    }

    public override void Attack()
    {
        flame.Play();
    }

    private class UpgradeDamage : Upgrade
    {
        private new Flamethower tower;

        readonly int[] cost = new int[5] { 50, 60, 70, 90, 150 };

        public UpgradeDamage()
        {
            maxLevel = cost.Length;
            tower = (Flamethower) base.tower; // Safe
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
            tower.flameProj.damage += 0.1f;
        }
    }
}
