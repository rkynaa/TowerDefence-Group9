using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreEntity : PlaceableEntity
{
    private void Start()
    {
        EnemyEntity.core = this;
    }

    private void OnDestroy()
    {
        // You Lose!
    }
}
