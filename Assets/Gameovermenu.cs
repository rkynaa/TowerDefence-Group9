using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameovermenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void GameoverBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);//Loads the next scene in the queue
        GameMaster.Reset();
    }

    public void Gameovermainmenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        GameMaster.Reset();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
