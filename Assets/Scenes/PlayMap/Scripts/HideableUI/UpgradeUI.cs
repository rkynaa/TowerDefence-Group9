using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public TowerEntity tower;
    public Upgrade upgrade;
    public Text name;
    public int index;

    public GameObject gameObject;
    bool active;
    void Start()
    {
        ListUpgradesAvail.upgradeUIs[index] = this;
    }

    public void initialize(TowerEntity tower, Upgrade upgrade)
    {
        this.tower = tower;
        this.upgrade = upgrade;
        // set upgrade name and cost
        name.text = upgrade.ToString() + " - $" + upgrade.GetCost();
    }

    public void onClick()
    {
        tower.ApplyUpgrade(upgrade);
    }
}
