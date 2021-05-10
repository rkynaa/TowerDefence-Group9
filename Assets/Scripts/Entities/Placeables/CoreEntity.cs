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

    protected override void CancelMove()
    {
        return;
    }

    protected override void Placed()
    {
        // Activate
    }

    private void OnDestroy()
    {
        // You Lose!
    }
}
