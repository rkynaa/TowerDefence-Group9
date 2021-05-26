using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    private TowerEntity tower;
    private Upgrade upgrade;
    private int cost;
    private Image background;

    public TextMeshProUGUI text;
    public int index;

    void Start()
    {
        TowerPanel.upgradeUIs[index] = this;
        background = GetComponent<Image>();
    }

    public void Setup(TowerEntity tower, Upgrade upgrade)
    {
        this.tower = tower;
        this.upgrade = upgrade;

        // set upgrade name and cost
        cost = upgrade.GetCost();
        text.text = upgrade.ToString() + "\n$" + cost;
    }

    public void Hide()
    {
        upgrade = null;
        background.color = GameMaster.red;
        text.text = "";
    }

    public void OnClick()
    {
        if(upgrade != null)
        {
            upgrade.BuyUpgrade(tower);
            TowerPanel.UpdateUpgrades();
        }
    }

    public void UpdateColor(int moneyAvailable)
    {
        if (moneyAvailable >= cost && upgrade != null)
        {
            background.color = GameMaster.green;
        }
        else
        {
            background.color = GameMaster.red;
        }
    }
}
