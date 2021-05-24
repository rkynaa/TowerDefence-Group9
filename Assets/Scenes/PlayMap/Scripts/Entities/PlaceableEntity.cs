using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaceableEntity : Entity
{
    [Header("PlaceableEntity")]
    public int cost = 0;

    public enum State
    {
        DISABLED,
        MOVING,
        ACTIVE
    }

    public State curState = State.DISABLED;

    private int collidedCount = 0;
    public bool ValidLocation { get { return collidedCount <= 0; } }

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

    /// <summary>
    /// When a move is canceled a tower can interupt the cancel if required.
    /// Inturupting a cancel should occur only if the tower is a required tower such as the core
    /// </summary>
    /// <returns>Whether to interupt</returns>
    public virtual bool CancelMove()
    {
        GameMaster.instance.GainMoney(cost);
        return false;
    }

    public virtual void Placed()
    {
        curState = State.ACTIVE;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        collidedCount++;
        if (curState == State.MOVING)
        {
            foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.color = Color.red;
            }
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        collidedCount--;
        if (curState == State.MOVING && collidedCount <= 0)
        {
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                spriteRenderers[i].color = startColor[i];
            }
        }
    }
}
