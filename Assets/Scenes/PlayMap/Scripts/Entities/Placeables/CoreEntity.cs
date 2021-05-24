using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//kushan
using UnityEngine.UI;

public class CoreEntity : PlaceableEntity
{
    public Text statDisplay;

    protected override void Start()
    {
        base.Start();

        GameMaster.instance.core = this;
    }

    public override bool CancelMove()
    {
        return true;
    }

    protected virtual void Update()
    {
        if (curState == State.MOVING)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!CancelMove())
                {
                    Destroy(gameObject);
                }
                return;
            }

            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            transform.position = newPos;
        }
    }

    protected virtual void OnMouseDown()
    {
        if (curState != State.MOVING)
        {
            curState = State.MOVING;
        }
    }

    protected virtual void OnMouseUp()
    {
        if (ValidLocation && curState == State.MOVING)
        {
            curState = State.ACTIVE;
            Placed();
        }
    }

    public override void Placed()
    {
        base.Placed();
        // Activate
    }

    private void OnDestroy()
    {
        // You Lose!
        BuildManager.instance.StartCoroutine(GameOver(2f));
    }

    private IEnumerator GameOver(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//kushan
        //not working properly. Gamemasterinstance cant figure out. stats
        statDisplay.text =
            "Enemies Killed: " + GameMaster.instance.stats.enemiesKilled.ToString();// + "\n" +
           //"Bosses Killed: " + GameMaster.instance.stats.bossesKilled + "\n" +
           //"Towers Placed: " + GameMaster.instance.stats.towersPlaced + "\n" +
           //"Towers Lost:  " + GameMaster.instance.stats.towersLost + "\n" +
           // "Towers Cost:  " + GameMaster.instance.stats.towersCost + "\n" +
           // "Repair Count:  " + GameMaster.instance.stats.repairCount + "\n" +
           // "Repair Amount:  " + GameMaster.instance.stats.repairAmount + "\n" +
           // "Repair Cost:  " + GameMaster.instance.stats.repairCost + "\n" + "\n" +
           // "Money Generated:  " + GameMaster.instance.stats.moneyGenerated + "\n" +
           // "Damage Dealt:  " + GameMaster.instance.stats.damageDealt + "\n" +
           // "Damage Taken:  " + GameMaster.instance.stats.damageTaken

    }

    
}

