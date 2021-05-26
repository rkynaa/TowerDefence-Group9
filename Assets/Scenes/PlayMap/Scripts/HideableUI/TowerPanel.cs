using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerPanel : MonoBehaviour
{

    public UpgradeUI[] upgradeUIs;
    public static TowerEntity tower;

    [HideInInspector]
    public static bool active = false;

    public static TowerPanel instance;

    public TextMeshProUGUI towerName;
    public Image sellButton;
    [HideInInspector]
    public TextMeshProUGUI sellText;
    public Image repairButton;
    [HideInInspector]
    public TextMeshProUGUI repairText;

    public void Start()
    {
        instance = this;
        sellText = sellButton.GetComponentInChildren<TextMeshProUGUI>();
        repairText = repairButton.GetComponentInChildren<TextMeshProUGUI>();
        sellButton.color = GameMaster.green;
        Hide();
    }

    public static void Setup(TowerEntity tower)
    {
        if(active)
        {
            active = false;
            TowerPanel.tower.CloseUI();
        }
        TowerPanel.tower = tower;
        instance.towerName.text = tower.name;
        Show();
        UpdateUpgrades();
    }

    public static void UpdateUpgrades()
    {
        for (int i = 0; i < instance.upgradeUIs.Length; i++)
        {
            if(i < tower.upgradesAvailable.Count)
            {
                instance.upgradeUIs[i].Setup(tower, tower.upgradesAvailable[i]);
            }
            else
            {
                instance.upgradeUIs[i].Hide();
            }
        }
        UpdateUI();
    }

    public static void UpdateUI()
    {
        if(active)
        {
            int moneyAvailable = GameMaster.instance.GetMoney();

            int sellPrice = tower.GetSellPrice();
            instance.sellText.text = "Sell $" + sellPrice;

            int repairPrice = tower.GetRepairPrice();
            instance.repairText.text = "Repair $" + repairPrice;
            if (moneyAvailable >= sellPrice)
            {
                instance.repairButton.color = GameMaster.green;
            }
            else
            {
                instance.repairButton.color = GameMaster.red;
            }

            foreach (UpgradeUI ui in instance.upgradeUIs)
            {
                ui.UpdateColor(moneyAvailable);
            }
        }
    }

    public static void Hide()
    {
        active = false;
        instance.gameObject.SetActive(false);
        if (tower != null)
        {
            tower.CloseUI();
        }
    }

    public static void Show()
    {
        active = true;
        instance.gameObject.SetActive(true);
    }

    public void Sell()
    {
        tower.SellTower();
        Hide();
    }

    public void Repair()
    {
        tower.RepairTower();
        UpdateUI();
    }
}
