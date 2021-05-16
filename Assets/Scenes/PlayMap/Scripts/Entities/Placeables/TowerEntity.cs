using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerEntity : PlaceableEntity
{
    [Header("Tower Entity")]

    protected Entity target;

    [Header("Attributes")]

    public int cost = 50;
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

    public string enemyTag = "Enemy";

    public Transform partToRotate = null;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        rangeCircle.Initialise(this, Range);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        if (partToRotate == null)
        {
            partToRotate = gameObject.transform;
        }
    }

    protected override void Placed()
    {
        tag = "Tower";
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // Inefficient but easy to implement for the prototype

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= Range)
        {
            target = nearestEnemy.GetComponent<EnemyEntity>();
        } 
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (target == null || curState != State.ACTIVE)
            return;

        Vector3 dir = target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.back);
        partToRotate.rotation = new Quaternion(0, 0, rotation.z, rotation.w); // look towards target before attacking

        if (attackCountdown <= 0f)
        {
            if (target != null)
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

    public virtual void Attack()
    {
        GameObject projectileGO = Instantiate(attackProjectile.gameObject, firePoint.position, partToRotate.rotation);
        Projectile proj = projectileGO.GetComponent<Projectile>();
        proj.callbackUpgrades = upgrades;
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
        if(isUIOpen)
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
