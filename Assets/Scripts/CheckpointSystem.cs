using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class CheckpointSystem : MonoBehaviour
{
    [SerializeField] Transform currentCheckpoint { get; set; }
    [SerializeField] Transform levelStartPoint { get; set; }

    PlayerInputActions playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = InputActionSingleton.instance.playerInputActions;
        playerInput.Player.RestartRun.performed += InputRestart;
    }

    private void InputRestart(InputAction.CallbackContext context)
    {
        if (context.interaction is HoldInteraction)
        {
            SetPlayerPosition(levelStartPoint);
        }
        else if (context.interaction is TapInteraction)
        {
            SetPlayerPosition(currentCheckpoint);
        }
    }

    public void SetPlayerPosition(Transform newTransform)
    {
        transform.position = newTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            Checkpoint.CheckpointType checkpointType = collision.gameObject.GetComponent<Checkpoint>().checkpointType;

            switch (checkpointType.ToString())
            {
                case "ScreenStart":
                    currentCheckpoint = collision.gameObject.transform;
                    break;

                case "ScreenEnd":
                    currentCheckpoint = collision.gameObject.GetComponent<Checkpoint>().nextCheckpoint;
                    SetPlayerPosition(currentCheckpoint); 
                    break;

                default:

                    break;
            }
        }
    }
}
