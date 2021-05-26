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

        if(!enableTracking || !active)
        {
            return;
        }

        if(target == null)
        {
            GetNewTarget(null);
        }

        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;

            Quaternion rotation = Quaternion.LookRotation(dir, transform.TransformDirection(Vector3.back));
            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }
    }

    protected override void HitTarget(EnemyEntity target)
    {
        base.HitTarget(target);

        if (pierce > 0)
        {
            GetNewTarget(target.transform);
        }
    }

    private void GetNewTarget(Transform old_target)
    {
        float shortestDistance = Mathf.Infinity;
        EnemyEntity nearestEnemy = null;

        foreach (EnemyEntity enemy in GameMaster.instance.enemiesAlive)
        {
            if(enemy.transform != old_target)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
    }
}
