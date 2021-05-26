using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    public static GameObject gameObject;

    public static void OpenClosePanel(bool active)
    {
        if (active == false)
        {
            gameObject.transform.gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.gameObject.SetActive(false);
        }
    }
}
