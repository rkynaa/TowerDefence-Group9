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
        AddUpgrade(new UpgradeSpeed());
    }

    protected override void Update()
    {
        base.Update();

        if (target == null && flameProj.particleSystem.isPlaying)
        {
            flameProj.particleSystem.Stop();
        }
    }

    public override void Attack()
    {
        flameProj.particleSystem.Play();
    }

    private class UpgradeDamage : Upgrade
    {
        readonly int[] cost = new int[] { 100, 150, 200, 300, 500 };

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
            ((Flamethower)tower).flameProj.damage += 0.4f;
        }
    }

    private class UpgradeSpeed : Upgrade
    {
        readonly int[] cost = new int[] { 100, 150, 200, 300, 500 };

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
            return "Speed";
        }

        public override void OnUpgrade()
        {
            Flamethower flamethower = (Flamethower)tower;
            flamethower.main.simulationSpeed += 0.5f;
        }
    }
}
