using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameProjectile : MonoBehaviour
{
    public float damage = 0.8f;

    private new ParticleSystem particleSystem;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    public enum Mode { NONE, ENEMY, TOWER }
    public Mode mode = Mode.NONE;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Entity target;

        switch (mode)
        {
            case Mode.ENEMY:
                target = other.GetComponent<PlaceableEntity>();
                break;
            case Mode.TOWER:
                target = other.GetComponent<EnemyEntity>();
                break;
            default:
                target = other.GetComponent<Entity>();
                break;
        }


        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

        if (target != null)
        {
            target.DamageEntity(damage * numCollisionEvents, true);
        }
    }
}
