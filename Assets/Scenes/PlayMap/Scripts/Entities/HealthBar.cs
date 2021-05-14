using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
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

    private Image healthFill;
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        healthFill = GetComponentsInChildrenByName<Image>("HealthFill");
        canvas = GetComponent<Canvas>();
        Hide();
    }

    public void Show()
    {
        canvas.enabled = true;
    }

    public void Hide()
    {
        canvas.enabled = false;
    }

    private T GetComponentsInChildrenByName<T>(string name) where T : Component
    {
        foreach (T component in GetComponentsInChildren<T>())
        {
            if (component.gameObject.name == name)
            {
                return component;
            }
        }
        return null;
    }
}
