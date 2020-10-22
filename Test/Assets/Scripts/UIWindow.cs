using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    
    protected GameObject content;

    [SerializeField] protected GameState state;

    protected virtual void Awake()
    {
        gameObject.SetActive(true);
        content = transform.GetChild(0).gameObject;

        GameStateManager.Instance.OnStateChange += CheckState;
    }

    protected virtual void CheckState(GameState state)
    {
        if(this.state == state)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    protected virtual void Show()
    {
        content.SetActive(true);
    }

    protected virtual void Hide()
    {
        content.SetActive(false);
    }
}
