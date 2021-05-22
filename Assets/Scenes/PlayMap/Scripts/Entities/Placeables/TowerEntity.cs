using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerEntity : PlaceableEntity
{
    [Header("Tower Entity")]

    public new string name;
    protected Entity target;

    /// <summary>
    /// If this tower can attack and target enemies
    /// </summary>
    public bool canAttack = true;

    [Header("Attributes")]

    [SerializeField]
    private float range = 4f;
    public float Range
    {
        get { return range; }
        set
        {
            range = value;
            rangeCircle.SetRange(range);
        }
    }
    public float attackSpeed = 1f;

    private float attackCountdown = 0f;

    [Header("Upgrades")]

    protected HashSet<Upgrade> upgrades = new HashSet<Upgrade>();
    protected List<Upgrade> upgradesAvailable = new List<Upgrade>();

    [Header("Unity Fields")]

    public Projectile attackProjectile;
    public Transform firePoint;
    public RangeCircle rangeCircle;

    public ListUpgradesAvail displayUpgList;

    public string enemyTag = "Enemy";

    public Transform partToRotate = null;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        rangeCircle.Initialise(this, Range);

        if (canAttack)
        {
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }

        if (partToRotate == null)
        {
            partToRotate = gameObject.transform;
        }
    }

    public override bool OnDeath()
    {
        GameMaster.instance.stats.towersLost += 1;
        return true;
    }

    public override bool OnDamage(float damage)
    {
        GameMaster.instance.stats.damageTaken += damage;
        return base.OnDamage(damage);
    }

    public virtual int GetSellPrice()
    {
        return (int)(cost * 0.8 * (Health / MaxHealth));
    }

    public void SellTower()
    {
        Destroy(gameObject);
        GameMaster.instance.GainMoney(GetSellPrice());
    }

    public virtual int GetRepairPrice()
    {
        return (int)(cost * 0.35 * GameMaster.instance.costDifficulty);
    }

    public void RepairTower()
    {
        int repairPrice = GetRepairPrice();
        if (GameMaster.instance.SpendMoney(repairPrice))
        {
            GameMaster.instance.stats.repairCount += 1;
            GameMaster.instance.stats.repairCost += repairPrice;
            GameMaster.instance.stats.repairAmount += MaxHealth - Health;
            Health = MaxHealth;
        }
    }

    public override void Placed()
    {
        base.Placed();
        tag = "Tower";
    }

    void UpdateTarget()
    {
        if (!canAttack)
        {
            return;
        }

        float shortestDistance = Mathf.Infinity;
        EnemyEntity nearestEnemy = null;

        foreach (EnemyEntity enemy in GameMaster.instance.enemiesAlive)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= Range)
        {
            target = nearestEnemy;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!canAttack || target == null || curState != State.ACTIVE)
            return;

        Vector3 dir = target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.back);
        partToRotate.rotation = new Quaternion(0, 0, rotation.z, rotation.w); // look towards target before attacking

        if (attackCountdown <= 0f)
        {
            foreach (Upgrade upgrade in upgrades)
            {
                upgrade.OnAttack();
            }
            Attack();
            attackCountdown = 1 / attackSpeed;
        }

        attackCountdown -= Time.deltaTime;
    }

    // Possible performance improvements over the current targeting system.
    public virtual void OnRangeEnter(Collider2D collision)
    {

    }

    public virtual void OnRangeExit(Collider2D collision)
    {

    }

    public virtual void OnRangeStay(Collider2D collision)
    {

    }

    public override void OnHit(Entity target, float damage)
    {
        EnemyEntity enemy = (EnemyEntity)target; // safe

        GameMaster.instance.stats.damageDealt += damage;

        foreach (Upgrade upgrade in upgrades)
        {
            upgrade.OnHit(enemy);
        }
    }

    public virtual void Attack()
    {
        GameObject projectileGO = Instantiate(attackProjectile.gameObject, firePoint.position, partToRotate.rotation);
        Projectile proj = projectileGO.GetComponent<Projectile>();
        proj.Fire(target.transform);
    }

    /// <summary>
    /// Adds an upgrade to be bought from the upgrade menu 
    /// </summary>
    /// <param name="upgrade"></param>
    public void AddUpgrade(Upgrade upgrade)
    {
        upgradesAvailable.Add(upgrade);
    }

    /// <summary>
    /// Removes an upgrade to no longer be able to buy from the upgrade menu 
    /// </summary>
    /// <param name="upgrade"></param>
    public void RemoveUpgrade(Upgrade upgrade)
    {
        upgradesAvailable.Remove(upgrade);
    }

    /// <summary>
    /// Upgrades the tower with a new upgrade
    /// </summary>
    /// <param name="upgrade"></param>
    public void ApplyUpgrade(Upgrade upgrade)
    {
        upgrades.Add(upgrade);
        upgrade.OnUpgrade();
    }

    public void OpenUI()
    {
        isUIOpen = true;
        rangeCircle.Show();

    }

    public void CloseUI()
    {
        isUIOpen = false;
        rangeCircle.Hide();
    }

    private bool isUIOpen = false;
    protected void OnMouseUpAsButton()
    {
        if (isUIOpen || curState != State.ACTIVE)
        {
            // Close ui
            CloseUI();
        }
        else
        {
            // Open ui
            OpenUI();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
