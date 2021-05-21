using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListUpgradesAvail : HideableUI
{
    public TowerEntity main_tower;
    public List<Upgrade> upgradesList;

    public void initialize(TowerEntity tower, List<Upgrade> upgrade_list)
    {
        main_tower = tower;
    }

    public void printList();
    public override void Hide()
    {

    }

    public override void Show()
    {

    }
}
