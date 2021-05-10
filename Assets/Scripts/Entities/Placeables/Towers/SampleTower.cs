using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTower : TowerEntity
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        base.Attack();
        // target.DamageEntity(5);
    }
}
