using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Singleton pattern
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager created!");
            return;
        }
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

    private void Update()
    {
        if (toBuild != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!toBuild.CancelMove())
                {
                    Destroy(toBuild);
                    toBuild = null;
                    return;
                }
                // else ignore the cancel attempt
            }

            // 0 = left, 1 = right, 2 = middle
            if (Input.GetMouseButtonUp(0))
            {
                if (toBuild.ValidLocation)
                {
                    if (GameMaster.SpendMoney(toBuild.cost * GameMaster.costDifficulty))
                    {
                        toBuild.Placed();
                    }
                    toBuild = null;
                    return;
                }
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
        if(GameMaster.HasMoney(tower.cost * GameMaster.costDifficulty))
        {
            toBuild = tower;
        }
    }
}
