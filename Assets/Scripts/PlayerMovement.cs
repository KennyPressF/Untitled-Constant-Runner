using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

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
            ChangeDirection();
            sideHitChecker.ChangeColliderSide();
        }
    }

    public void ChangeDirection()
    {
        playerVisuals.FlipSprite();
        moveSpeed = -moveSpeed;
    }

    
}

