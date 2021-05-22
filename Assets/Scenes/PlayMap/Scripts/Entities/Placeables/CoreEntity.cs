using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//kushan
using UnityEngine.UI;

public class CoreEntity : PlaceableEntity
{
    protected override void Start()
    {
        base.Start();

        GameMaster.instance.core = this;
    }

    public override bool CancelMove()
    {
        return true;
    }

    public override void Placed()
    {
        base.Placed();
        // Activate
    }

    private void OnDestroy()
    {
        // You Lose!
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//kushan

    }

    
}
