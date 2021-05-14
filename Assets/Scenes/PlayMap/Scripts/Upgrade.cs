public abstract class Upgrade
{
    protected TowerEntity tower;
    protected int level = 0;
    protected int maxLevel;

    /// <summary>
    /// Gets the name of the upgrade
    /// </summary>
    /// <returns>Name</returns>
    public abstract string GetName();

    /// <summary>
    /// Gets the name of the upgrade for displaying in the shop
    /// </summary>
    /// <returns>name + level</returns>
    public override string ToString()
    {
        if (maxLevel > 0 && level >= 0)
        {
            return GetName() + " " + Roman.To(level + 1); // Display name with level
        }
        return GetName();
    }

    /// <summary>
    /// <para>
    /// Gets the cost in [INSERT_CURRENCY_NAME].
    /// </para>
    /// Called before level is incremented. 
    /// level = 0 when upgrading from 0 -> 1.
    /// </summary>
    /// <returns>price of upgrade</returns>
    public abstract int GetCost();

    /// <summary>
    /// Called before a tower is upgraded
    /// </summary>
    /// <param name="tower"></param>
    public void BuyUpgrade(TowerEntity tower)
    {
        this.tower = tower; // Save the tower here for use in OnAttack() or OnHit() if required

        //TODO: subtract money here with GetCost() 

        tower.RemoveUpgrade(this); // Remove upgrade to prevent the upgrade from being bought twice
        tower.ApplyUpgrade(this);

        if (level < maxLevel)
        {
            level += 1;
            tower.AddUpgrade(this); // Add the next level of this upgrade
        }
    }

    /// <summary>
    /// Called after a tower is upgraded
    /// </summary>
    public virtual void OnUpgrade() { }

    /// <summary>
    /// Called before a projectile is fired
    /// </summary>
    public virtual void OnAttack() { }

    /// <summary>
    /// Called after a projectile hits it's target before any damage is applied.
    /// </summary>
    /// <param name="target"></param>
    public virtual void OnHit(EnemyEntity target) { }
}