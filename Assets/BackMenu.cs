using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackMenu : MonoBehaviour
{
    public void BackGame()
    {
        GameMaster.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);//Loads the next scene in the queue
        GameMaster.Reset();
    }
}
