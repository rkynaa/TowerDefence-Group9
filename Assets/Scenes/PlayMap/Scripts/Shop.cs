using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public static List<Shop> shops = new List<Shop>();

    public TowerEntity tower;

    public TextMeshProUGUI costText;
    public TextMeshProUGUI nameText;

    private Image background;

    public Color green = Color.green;
    public Color red = Color.red;

    public void Start()
    {
        costText.text = "Cost: " + tower.Cost;
        nameText.text = tower.name;
        background = GetComponent<Image>();
        shops.Add(this);
        UpdateColor(GameMaster.instance.GetMoney());
    }

    public void OnDestroy()
    {
        shops.Remove(this);
    }

    public void Click()
    {
        costText.text = "Cost: " + tower.Cost;
        BuildManager.instance.BuildTower(tower);
    }

    public static void UpdateColor()
    {
        int moneyAvailable = GameMaster.instance.GetMoney();
        foreach (Shop shop in shops)
        {
            shop.UpdateColor(moneyAvailable);
        }
    }

    public void UpdateColor(int moneyAvailable)
    {
        if(moneyAvailable >= tower.Cost)
        {
            background.color = green;
        }
        else
        {
            background.color = red;
        }
    }
}
