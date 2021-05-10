using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    
    private float _health = 10;
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

    [SerializeField]
    private float _maxHealth = 10;
    public float MaxHealth { get { return _maxHealth; } }

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

    protected virtual void Start()
    {
        Health = _maxHealth;
    }

    /// <summary>
    /// Reduces the entity's health by damage. Can also be used to heal the entity
    /// </summary>
    /// <param name="damage">The amount to reduce the entity's health by</param>
    public void DamageEntity(float damage)
    {
        Health -= damage;
        Debug.Log(name + ": Health = " + Health);
    }

    /// <summary>
    /// Called when the entity's health is reduced to 0 (or less)
    /// </summary>
    /// <returns>If the entity died.</returns>
    public virtual bool OnDeath()
    {
        Debug.Log(name + ": Died");
        return true; 
    }
}
