using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour
{
    [Header("Entity")]
    public HealthBar healthBar;

    private float _health;
    float Health { 
        get { return _health; } 
        set 
        {
            if (IsDead)
            {
                return;
            }

            _health = value;
            if (_health <= 0)
            {
                IsDead = OnDeath();
            }
        } 
    }

    [SerializeField]
    private float _maxHealth = 10;
    public float MaxHealth { get { return _maxHealth; } }

    bool _isDead = false;
    bool IsDead { get { return _isDead; }
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
        Health = MaxHealth;
        if(healthBar == null)
        {
            healthBar = GetComponentInChildren<HealthBar>();
        }
        healthBar.maxHealth = MaxHealth;
    }

    /// <summary>
    /// Reduces the entity's health by damage. Can also be used to heal the entity
    /// </summary>
    /// <param name="damage">The amount to reduce the entity's health by</param>
    public void DamageEntity(float damage)
    {
        Health -= damage;
        healthBar.Value = Health;
    }

    /// <summary>
    /// Called when the entity's health is reduced to 0 (or less)
    /// </summary>
    /// <returns>If the entity died.</returns>
    public virtual bool OnDeath()
    {
        return true; 
    }
}
