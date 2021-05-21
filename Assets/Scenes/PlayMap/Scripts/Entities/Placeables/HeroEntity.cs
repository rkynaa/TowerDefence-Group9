using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroEntity : TowerEntity
{
    /// <summary>
    /// Current Level
    /// </summary>
    int level = 1;

    /// <summary>
    /// Max Level
    /// </summary>
    int maxLevel = 100;

    /// <summary>
    /// Current Experience
    /// </summary>
    float experience = 0;

    /// <summary>
    /// Experience required to level up
    /// </summary>
    int nextLevelExp;

    /// <summary>
    /// Experience multiplier increase or decrease based on upgrades or difficulty level
    /// </summary>
    float expMultiplier = 1;

    protected override void Start()
    {
        base.Start();
        nextLevelExp = CalcNextLevelExp();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void OnHit(Entity target, float damage)
    {
        base.OnHit(target, damage);

        AddExperience(damage);
    }

    public void AddExperience(float exp)
    {
        experience += exp;
        if (experience >= nextLevelExp)
        {
            if(CanLevelUp())
            {
                LevelUp();
            } 
            else
            {
                experience = nextLevelExp; // Cap exp
            }
            
        }
    }

    private void LevelUp()
    {
        level++;
        experience -= nextLevelExp;
        nextLevelExp = CalcNextLevelExp();
        OnLevelUp();
    }

    protected abstract void OnLevelUp();

    public bool CanLevelUp()
    {
        if((level + 1) % 10 != 0)
        {
            return true;
        }
        return false;
    }

    private int CalcNextLevelExp()
    {
        return (int) (expMultiplier * 90.9 * Mathf.Pow(1.2f, level));
    }
}
