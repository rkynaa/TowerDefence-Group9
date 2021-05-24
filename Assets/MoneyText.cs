using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text text = GetComponent<Text>();
        GameMaster.instance.moneyText = text;
        text.text = GameMaster.instance.GetMoney().ToString();//kushan
    }
}
