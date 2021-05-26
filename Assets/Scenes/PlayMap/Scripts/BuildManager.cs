using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Singleton pattern
    public static BuildManager instance;

    private void Awake()
    {
        instance = this;
    }

    public PlaceableEntity toBuild { get; private set; }

    // Prefabs of all towers
    public TowerEntity[] towers;

    // Start is called before the first frame update
    void Start()
    {
        foreach (TowerEntity tower in towers)
        {
            // Import tower into shop
            // Create squares
        }
    }

    private bool leftHeld = false;

    private void Update()
    {
        for (int i = 0; i < towers.Length || i >= 9; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                BuildTower(towers[i]);
            }
        }

        if (toBuild != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonUp(1))
            {
                if (!toBuild.CancelMove())
                {
                    Destroy(toBuild.gameObject);
                    toBuild = null;
                    return;
                }
                // else ignore the cancel attempt
            }

            // 0 = left, 1 = right, 2 = middle
            if (leftHeld && Input.GetMouseButtonUp(0))
            {
                if (toBuild.ValidLocation)
                {
                    int cost = toBuild.Cost;
                    if (GameMaster.instance.SpendMoney(cost))
                    {
                        Debug.Log("Building tower");
                        GameMaster.instance.stats.towersCost += cost;
                        GameMaster.instance.stats.towersPlaced += 1;
                        toBuild.Placed();
                        toBuild = null;
                    }
                    return;
                }
            }

            if (Input.GetMouseButton(0))
            {
                leftHeld = true;
            }
            else
            {
                leftHeld = false;
            }

            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            toBuild.transform.position = newPos;
        }
    }

    /// <summary>
    /// Handles placing, building, and cost of towers
    /// </summary>
    /// <param name="tower">The tower to build</param>
    public void BuildTower(TowerEntity tower)
    {
        Debug.Log("Building tower " + tower.Cost);
        if (GameMaster.instance.HasMoney(tower.Cost))
        {
            Debug.Log("Building tower 2");
            if (toBuild != null)
            {
                if (!toBuild.CancelMove())
                {
                    Destroy(toBuild.gameObject);
                }
                else
                {
                    // Cannot cancel building the current building!
                    return;
                }
            }

            Debug.Log("Building tower 3");
            leftHeld = false;
            toBuild = Instantiate(tower);
            toBuild.curState = PlaceableEntity.State.MOVING;
        }
    }
}
