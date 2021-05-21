using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// These upgrades get their OnHit() methods called
    /// </summary>
    [HideInInspector]
    public Entity source = null; 

    protected Transform target;
    protected Vector2 direction;

    public float speed = 4f;
    public float damage = 5f;

    public float lifeTime = 1f;

    public void Fire(Transform target)
    {
        this.target = target;
        // direction = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
            return;
        }
        lifeTime -= Time.deltaTime;

        float step = speed * Time.deltaTime;

        // Vector3 dir = target.transform.position - transform.position;

        // Quaternion rotation = Quaternion.LookRotation(dir, transform.TransformDirection(Vector3.back));
        // transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        // move towards the target
        transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.up, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyEntity enemy = collision.gameObject.GetComponentInParent<EnemyEntity>();

        if (enemy == null)
        {
            // Hit tower or some other thing
            return;
        }

        if(source != null)
        {
            source.OnHit(enemy, damage);
        }
        
        HitTarget(enemy);
    }

    protected virtual void HitTarget(EnemyEntity target)
    {
        target.DamageEntity(damage);
        Destroy(gameObject);
    }
}
