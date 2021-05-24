using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsDisplay : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Text text = GetComponent<Text>();
        Statistics s = GameMaster.instance.stats;
        text.text = "Enemies Killed: " + s.enemiesKilled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
