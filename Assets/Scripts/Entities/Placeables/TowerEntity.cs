using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEntity : PlaceableEntity
{
    protected Entity target;

    [Header("Attributes")]

    public float range = 4f;
    public float attackSpeed = 1f;
    public float attackCountdown = 0f;

    [Header("Unity Fields")]

    public GameObject attackProjectile;
    public Transform firePoint;

    public string enemyTag = "Enemy";

    public Transform partToRotate = null;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        if (partToRotate == null)
        {
            partToRotate = gameObject.transform;
        }
    }

    protected override void Placed()
    {
        tag = "Tower";
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.GetComponent<EnemyEntity>();
        } 
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (target == null || curState != State.ACTIVE)
            return;

        Vector3 dir = target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.back);
        partToRotate.rotation = new Quaternion(0, 0, rotation.z, rotation.w); // look towards target before attacking

        if (attackCountdown <= 0f)
        {
            _Attack();
            attackCountdown = 1 / attackSpeed;
        }

        attackCountdown -= Time.deltaTime;
    }

    void _Attack()
    {
        if (target == null)
            return;

        Attack();
    }

    public virtual void Attack()
    {
        GameObject projectileGO = Instantiate(attackProjectile, firePoint.position, partToRotate.rotation);
        Projectile proj = projectileGO.GetComponent<Projectile>();
        proj.Fire(target.transform);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
