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

    private PlaceableEntity toBuild;
    public PlaceableEntity ToBuild
    {
        get { return toBuild; }
    }

    // Prefabs of all towers
    public struct Towers
    {
        public static TowerEntity sampleTower;
    }

    // Start is called before the first frame update
    void Start()
    {
        toBuild = Towers.sampleTower;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
