using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCircle : HideableUI
{
    public TowerEntity attachedTower;
    private Vector2 scale;

    [Header("Presets")]
    public SpriteRenderer circle;

    public void Initialise(TowerEntity tower, float range)
    {
        attachedTower = tower;
        scale = attachedTower.transform.localScale;
        
        SetRange(range);
    }

    public void SetRange(float range)
    {
        transform.localScale = new Vector2(range * 2 / scale.x, range * 2 / scale.y);
    }

    public override void Hide()
    {
        circle.enabled = false;
    }

    public override void Show()
    {
        circle.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        attachedTower.OnRangeEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        attachedTower.OnRangeExit(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        attachedTower.OnRangeStay(collision);
    }
}
