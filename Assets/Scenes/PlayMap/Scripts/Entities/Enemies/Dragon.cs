using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : EnemyEntity
{
    [Header("Dragon")]
    public ParticleSystem flame;
    private ParticleSystem.MainModule main;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        main = flame.main;
        main.duration = (1 / attackSpeed);
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

    public override bool OnDeath()
    {
        GameMaster.instance.stats.bossesKilled += 1;
        return base.OnDeath();
    }
}
