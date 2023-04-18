using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] int remainingTurns;

    Rigidbody2D myRB;
    PlayerVisuals playerVisuals;
    SideHitChecker sideHitChecker;
    PlayerInputActions playerInput;

    private void Start()
    {
        playerInput = InputActionSingleton.instance.playerInputActions;
        playerVisuals = GetComponentInChildren<PlayerVisuals>();

        myRB = GetComponent<Rigidbody2D>();
        sideHitChecker = GetComponentInChildren<SideHitChecker>();
    }

    private void Update()
    {
        TurnAround();
    }

    private void FixedUpdate()
    {
        myRB.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, myRB.velocity.y);
    }

    private void TurnAround()
    {
        if (playerInput.Player.TurnAround.WasPressedThisFrame())
        {
            if(remainingTurns <= 0)
            {
                //SOMETHING TO TELL PLAYER THERE ARE NO TURNS LEFT
                return;
            }

            ChangeDirection();
            remainingTurns--;
            sideHitChecker.ChangeColliderSide();
        }
    }

    public void ChangeDirection()
    {
        playerVisuals.FlipSprite();
        moveSpeed = -moveSpeed;
    }

    
}

