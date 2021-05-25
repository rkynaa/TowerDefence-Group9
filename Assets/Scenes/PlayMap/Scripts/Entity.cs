using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour
{
    [Header("Entity")]
    public HealthBar healthBar;

    public AudioClip deathSound;
    public AudioClip damageSound;

    private float _health;
    public float Health { 
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
    public float MaxHealth { get { return _maxHealth; }
        set 
        {
            _maxHealth = value;
            healthBar.maxHealth = value;
        }
    }

    bool _isDead = false;
    bool IsDead { get { return _isDead; }
        set
        {
            _isDead = value;
            if (_isDead)
            {
                if(deathSound != null)
                {
                    PlayClip(deathSound);
                }
                Destroy(gameObject);
            }
        }
    }

    private void PlayClip(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, -10), GameMaster.volume);
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
    public void DamageEntity(float damage, bool noSound = false)
    {
        if (OnDamage(damage))
        {
            Health -= damage;
            healthBar.Value = Health;

            if (!noSound && damageSound != null && damage > 0)
            {
                PlayClip(damageSound);
            }
        }
    }

    /// <summary>
    /// Called before the entity's health is reduced to 0 (or less)
    /// </summary>
    /// <returns>If the entity died.</returns>
    public virtual bool OnDeath()
    {
        return true; 
    }

    /// <summary>
    /// Called before the entity's health is reduced
    /// </summary>
    /// <param name="damage">The amount the entity is damaged by</param>
    /// <returns>Whether to cancel the event</returns>
    public virtual bool OnDamage(float damage)
    {
        return true;
    }

    /// <summary>
    /// Called whenever the entity successfully damages a target
    /// </summary>
    /// <param name="damage">The amount of damage delt</param>
    public virtual void OnHit(Entity target, float damage) { }
}
