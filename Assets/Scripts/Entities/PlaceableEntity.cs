using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaceableEntity : Entity
{
    [SerializeField] bool Moveable = true;
    [SerializeField] bool Spawner = true;
    bool isPlaced = false;

    private void OnMouseDrag()
    {
        if (!isPlaced && Moveable)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            transform.position = newPos;
        }
    }

    private void OnMouseDown()
    {
        if(!isPlaced && Spawner)
        {
            Instantiate(this);
        }
    }

    private void OnMouseUp()
    {
        isPlaced = true;
    }
}
