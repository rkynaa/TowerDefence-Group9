using UnityEngine;
using UnityEngine.UI;

public abstract class HideableUI : MonoBehaviour
{
    public abstract void Show();

    public abstract void Hide();

    public T GetComponentInChildrenByName<T>(string name) where T : Component
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
