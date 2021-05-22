using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : EnemyEntity
{
    public Text test;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        target.DamageEntity(1);
    }
}
