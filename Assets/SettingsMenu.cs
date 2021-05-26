using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetVolume (float volume)
    {
        GameMaster.volume = volume;
    }

    public void Hard()
    {
        GameMaster.instance.costDifficulty = 1.5;
        GameMaster.instance.enemyDifficulty = 1.2;
        GameMaster.instance.money = 700;
    }

    public void Easy()
    {
        GameMaster.instance.costDifficulty = 1;
        GameMaster.instance.enemyDifficulty = 1;
        GameMaster.instance.money = 900;
    }
}
