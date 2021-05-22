using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameProjectile : MonoBehaviour
{
    public float damage = 0.8f;

    private new ParticleSystem particleSystem;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        PlaceableEntity tower = other.GetComponent<PlaceableEntity>();

        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

        if (tower != null)
        {
            tower.DamageEntity(damage * numCollisionEvents, true);
        }
    }
}
