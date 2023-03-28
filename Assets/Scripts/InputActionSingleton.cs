using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputActionSingleton : MonoBehaviour
{
    public PlayerInputActions playerInputActions;

    public static InputActionSingleton instance;

    private void Awake()
    {
        SetUpSingleton();
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void SetUpSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
