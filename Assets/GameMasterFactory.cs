using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterFactory : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameMaster.Awake();
    }
}
