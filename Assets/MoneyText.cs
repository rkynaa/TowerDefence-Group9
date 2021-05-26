using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyText : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameMaster.instance.moneyText = GetComponent<TextMeshProUGUI>();
    }
}
