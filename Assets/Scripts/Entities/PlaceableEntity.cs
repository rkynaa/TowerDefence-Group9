using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaceableEntity : Entity
{
    public bool Moveable = true;
    public bool Spawner = true;

    public enum State
    {
        DISABLED,
        MOVING,
        ACTIVE
    }

    public State curState = State.DISABLED;

    private bool validLocation = true;

    private Color[] startColor;

    protected override void Start()
    {
        base.Start();

        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        startColor = new Color[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            startColor[i] = spriteRenderers[i].color;
        }
    }

    protected virtual void Update()
    {
        if(curState == State.MOVING)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                CancelMove();
                return;
            }

            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            transform.position = newPos;
        }
    }

    protected virtual void CancelMove()
    {
        Destroy(gameObject);
    }

    protected void OnMouseDown()
    {
        if(Moveable)
        {
            if(Spawner)
            {
                GameObject copy = Instantiate(gameObject);
                PlaceableEntity copyScript = copy.GetComponent<PlaceableEntity>();
                copyScript.curState = State.MOVING;
                copyScript.Spawner = false;
            }
            else
            {
                curState = State.MOVING;
            }
        }
    }

    protected void OnMouseUp()
    {
        if(validLocation)
        {
            Moveable = false;
            curState = State.ACTIVE;
            Placed();
        }
    }

    protected abstract void Placed();

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(curState == State.MOVING)
        {
            validLocation = false;
            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.color = Color.red;
            }
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (curState == State.MOVING)
        {
            validLocation = true;
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].color = startColor[i];
            }
        }
    }
}
