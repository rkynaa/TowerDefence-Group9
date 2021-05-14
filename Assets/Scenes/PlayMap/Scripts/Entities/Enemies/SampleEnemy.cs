using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : EnemyEntity
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        target.DamageEntity(5);
    }
}
