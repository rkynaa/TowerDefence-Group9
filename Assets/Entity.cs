using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private float _health = 0;
    float Health { 
        get { return _health; } 
        set 
        {
            if (isDead)
            {
                return;
            }

            _health = value;
            if (_health <= 0)
            {
                isDead = OnDeath();
            }
        } 
    }

    private float maxHealth = 0;
    float GetMaxHealth { get { return maxHealth; } }

    bool _isDead = false;
    bool isDead { get { return _isDead; }
        set
        {
            _isDead = value;
            if (_isDead)
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Reduces the entity's health by damage. Can also be used to heal the entity
    /// </summary>
    /// <param name="damage">The amount to reduce the entity's health by</param>
    public void DamageEntity(int damage)
    {
        Health -= damage;
    }

    /// <summary>
    /// Called when the entity's health is reduced to 0 (or less)
    /// </summary>
    /// <returns>If the entity died.</returns>
    public virtual bool OnDeath()
    {
        return true; 
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
