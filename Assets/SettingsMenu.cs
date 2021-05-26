using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    
   
    // Start is called before the first frame update
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
        GameMaster.volume = volume;
    }

    public void Hard()
    {
        GameMaster.instance.costDifficulty = 1.5;
        GameMaster.instance.enemyDifficulty = 1.2;
    }

    public void Easy()
    {
        GameMaster.instance.costDifficulty = 1;
        GameMaster.instance.enemyDifficulty = 1;
    }
}
