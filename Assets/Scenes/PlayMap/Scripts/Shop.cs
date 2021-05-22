using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerEntity tower;

    public void Click()
    {
        BuildManager.instance.BuildTower(tower);
    }
}
