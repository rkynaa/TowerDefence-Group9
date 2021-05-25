using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TrackingProjectile : Projectile
{
    private TowerEntity tower;

    public bool enableTracking = true;

    public override void Fire(Transform target)
    {
        base.Fire(target);
        tower = (TowerEntity) source;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(!enableTracking)
        {
            return;
        }

        if (tower.target != null)
        {
            if (target != tower.target.transform)
            {
                target = tower.target.transform;
                lifeTime += 0.2f;
            }
        }
        else if(target == null)
        {
            return;
        }

        Vector3 dir = target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(dir, transform.TransformDirection(Vector3.back));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }
}
