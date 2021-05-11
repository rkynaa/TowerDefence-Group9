using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyEntity : Entity
{
    protected Entity target;
    public static CoreEntity core = null;

    public float targetingRange = 4f;
    public string targetTag = "Tower";

    public float attackingRange = 1f;
    public float attackSpeed = 1f;

    public float speed = 1f;

    public Transform partToRotate = null;

    // Start is called before the first frame update
    protected override void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        InvokeRepeating("_Attack", attackSpeed, attackSpeed);
        if (partToRotate == null)
        {
            partToRotate = gameObject.transform;
        }
        target = core;
        GameMaster.EnemiesAlive += 1;
    }

    float ManhattanDistance(Transform a, Transform b)
    {
        return Mathf.Abs(a.position.x - b.position.x) + Mathf.Abs(a.position.y - b.position.y);
    }

    void UpdateTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;

        foreach (GameObject t in targets)
        {
            float quickDistance = ManhattanDistance(transform, t.transform);
            if (quickDistance < shortestDistance)
            {
                float distanceToTarget = Vector3.Distance(transform.position, t.transform.position);
                if (distanceToTarget < shortestDistance)
                {
                    shortestDistance = distanceToTarget;
                    nearestTarget = t;
                }
            }
        }

        if (nearestTarget != null && shortestDistance <= targetingRange)
        {
            target = nearestTarget.GetComponent<Entity>();
        }
        else
        {
            target = core;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        float step = speed * Time.deltaTime;

        Vector3 dir = target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(dir, transform.TransformDirection(Vector3.up));
        partToRotate.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        // move towards the target
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
    }

    void _Attack()
    {
        if (target == null)
            return;

        float quickDistance = ManhattanDistance(transform, target.transform);
        if (quickDistance > attackingRange)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget > attackingRange)
            {
                return;
            }
        }

        Debug.Log(quickDistance);
        Attack();
    }

    public virtual void Attack()
    {
        target.DamageEntity(1);
    }

    public override bool OnDeath()
    {
        GameMaster.EnemiesAlive -= 1;
        return base.OnDeath();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, targetingRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackingRange);
    }
}
