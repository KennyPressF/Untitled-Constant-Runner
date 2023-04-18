using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class RunRestarter : MonoBehaviour
{
    [SerializeField] Transform screenStartPoint;
    [SerializeField] Transform levelStartPoint;
    
    PlayerInputActions playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = InputActionSingleton.instance.playerInputActions;
        playerInput.Player.RestartRun.performed += InputRestart;
    }

    private void InputRestart(InputAction.CallbackContext context)
    {
        if(context.interaction is HoldInteraction)
        {
            SetPlayerPosition(levelStartPoint);
        }
        else if(context.interaction is TapInteraction)
        {
            SetPlayerPosition(screenStartPoint);
        }
    }

    public void SetPlayerPosition(Transform newTransform)
    {
        transform.position = newTransform.position;
    }

    public void SetScreenStartPoint(Transform checkpoint)
    {
        screenStartPoint = checkpoint;
    }

    public void SetLevelStartPoint(Transform checkpoint)
    {
        levelStartPoint = checkpoint;
    }
}
