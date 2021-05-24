using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyEntity : Entity
{
    protected Entity target;
    protected float targetDistance;

    public float targetingRange = 4f;
    public string targetTag = "Tower";

    public float attackingRange = 1f;
    public float attackSpeed = 1f;

    public float speed = 1f;

    public int defeatReward = 1;

    public Transform partToRotate = null;

    // Start is called before the first frame update
    protected override void Start()
    {
        MaxHealth = (float) (MaxHealth * GameMaster.instance.enemyDifficulty);
        base.Start();
        if (GameMaster.instance.enemyDifficulty > 1)
        {
            attackSpeed = (float)(attackSpeed * GameMaster.instance.enemyDifficulty);
            speed = (float)(speed + 0.5 * speed * GameMaster.instance.enemyDifficulty);
        }

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        InvokeRepeating("CheckAttack", 1 / attackSpeed, 1 / attackSpeed);
        if (partToRotate == null)
        {
            partToRotate = gameObject.transform;
        }
        target = GameMaster.instance.core;
        GameMaster.instance.enemiesAlive.Add(this);
    }

    void UpdateTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;

        foreach (GameObject t in targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, t.transform.position);
            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                nearestTarget = t;
            }
        }

        if (nearestTarget != null && shortestDistance <= targetingRange)
        {
            target = nearestTarget.GetComponent<Entity>();
            targetDistance = shortestDistance;
        }
        else
        {
            target = GameMaster.instance.core;
            targetDistance = Vector3.Distance(transform.position, target.transform.position);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (target == null)
            return;

        float step = speed * Time.deltaTime;

        Vector3 dir = target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(dir, transform.TransformDirection(Vector3.back));
        partToRotate.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        if (targetDistance > attackingRange - 1)
        {
            // move towards the target
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
        }
    }

    void CheckAttack()
    {
        if (target == null)
            return;

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget > attackingRange)
        {
            return;
        }

        Attack();
    }

    public virtual void Attack()
    {
        target.DamageEntity(1);
    }

    public override bool OnDeath()
    {
        GameMaster.instance.stats.enemiesKilled += 1;

        GameMaster.instance.enemiesAlive.Remove(this);
        GameMaster.instance.GainMoney(defeatReward);
        return base.OnDeath();
    }

    private void OnDrawGizmosSelected()
    {
        // Target range in editor
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, targetingRange);

        // Attack range in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackingRange);
    }
}
