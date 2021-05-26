using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListUpgradesAvail : MonoBehaviour
{
    public static UpgradeUI[] upgradeUIs = new UpgradeUI[4];

    public GameObject gameObject;
    bool active;

    public static void initialize(TowerEntity tower, List<Upgrade> upgrade_list)
    {
        // for (int i = 0; i < upgradeUIs.Length; i++)
        // {
        //     upgradeUIs[i].initialize(tower, upgrade_list[i]);
        // }

        int i = 0;

        foreach (Upgrade item in upgrade_list)
        {
            upgradeUIs[i].initialize(tower, item);
            i++;

            // // Change color of button
            // var colors = GetComponent<Button>().colors;
            // colors.normalColor = Color.grey;
            // GetComponent<Button>().colors = colors;
        }
    }

    // public static void Hide()
    // {

    // }

    // public static void Show()
    // {

    // }
}
