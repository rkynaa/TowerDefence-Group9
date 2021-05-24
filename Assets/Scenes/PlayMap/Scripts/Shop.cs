using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TowerEntity tower;

    public Text costText;
    public Text nameText;

    public void Start()
    {
        costText.text = "Cost: " + tower.cost;
        nameText.text = tower.name;
    }

    public void Click()
    {
        costText.text = "Cost: " + tower.cost;
        BuildManager.instance.BuildTower(tower);
    }
}
