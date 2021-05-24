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

    public void Hard(double costDifficulty, double enemyDifficulty)
    {
        GameMaster.instance.costDifficulty = 4;
        GameMaster.instance.enemyDifficulty = 4;
    }

    public void Easy(double costDifficulty, double enemyDifficulty)
    {
        GameMaster.instance.costDifficulty = 0.5;
        GameMaster.instance.enemyDifficulty = 0.5;
    }
}
