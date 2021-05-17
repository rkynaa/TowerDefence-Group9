using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreEntity : PlaceableEntity
{
    protected override void Start()
    {
        base.Start();

        EnemyEntity.core = this;
    }

    public override bool CancelMove()
    {
        return true;
    }

    public override void Placed()
    {
        base.Placed();
        // Activate
    }

    private void OnDestroy()
    {
        // You Lose!
    }
}
