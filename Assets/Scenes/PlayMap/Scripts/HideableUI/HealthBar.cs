using UnityEngine;
using UnityEngine.UI;

public class HealthBar : HideableUI
{
    public float maxHealth = 1;
    public float Value 
    { 
        set 
        { 
            if(value < maxHealth)
            {
                Show();
                healthFill.fillAmount = value / maxHealth;
            }
            else
            {
                Hide();
            }
        } 
    }

    public Image healthFill;
    public Canvas canvas;

    public override void Show()
    {
        canvas.enabled = true;
    }

    public override void Hide()
    {
        canvas.enabled = false;
    }
}
