using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEntity : PlaceableEntity
{
    private Entity target;

    public float range = 4f;
    public string enemyTag = "Enemy";

    public float attackSpeed = 1f;

    public Transform partToRotate = null;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        InvokeRepeating("_Attack", attackSpeed, attackSpeed);
        if (partToRotate == null)
        {
            partToRotate = gameObject.transform;
        }
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
    void Update()
    {

    }

    void _Attack()
    {
        if (target == null)
            return;

        Vector3 dir = target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(dir, transform.TransformDirection(Vector3.up));
        partToRotate.rotation = new Quaternion(0, 0, rotation.z, rotation.w); // look towards target before attacking

        Attack();
    }

    public virtual void Attack()
    {
        target.DamageEntity(1);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
