using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTime;
    float jumpTimeCounter;
    bool isJumping;
    bool isGrounded;

    RaycastHit2D downHitRayCast;
    [SerializeField] float extraHeight;
    [SerializeField] LayerMask whatIsGround;

    Rigidbody2D myRB;
    BoxCollider2D myColl;
    PlayerVisuals playerVisuals;
    PlayerInputActions playerInput;

    private void Start()
    {
        playerInput = InputActionSingleton.instance.playerInputActions;

        myRB = GetComponent<Rigidbody2D>();
        myColl = GetComponent<BoxCollider2D>();
        playerVisuals = GetComponentInChildren<PlayerVisuals>();
    }

    private void Update()
    {
        CheckIfGrounded();
        ProcessJump();
        SetJumpAnimation();
    }

    private void ProcessJump()
    {
        if (playerInput.Player.Jump.WasPressedThisFrame() && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);
        }

        if (playerInput.Player.Jump.IsPressed())
        {
            if (jumpTimeCounter > 0 && isJumping)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (playerInput.Player.Jump.WasReleasedThisFrame())
        {
            isJumping = false;
        }

        DrawBoxCastBounds();
    }

    private void CheckIfGrounded()
    {
        downHitRayCast = Physics2D.BoxCast(myColl.bounds.center, myColl.bounds.size, 0, Vector2.down, extraHeight, whatIsGround);

        if (downHitRayCast.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded= false;
        }
    }

    private void DrawBoxCastBounds()
    {
        Debug.DrawRay(myColl.bounds.center + new Vector3(myColl.bounds.extents.x, 0), Vector2.down * (myColl.bounds.extents.y + extraHeight), Color.red);
        Debug.DrawRay(myColl.bounds.center - new Vector3(myColl.bounds.extents.x, 0), Vector2.down * (myColl.bounds.extents.y + extraHeight), Color.red);
        Debug.DrawRay(myColl.bounds.center - new Vector3(myColl.bounds.extents.x, myColl.bounds.extents.y + extraHeight), Vector2.right * (myColl.bounds.extents.x * 2), Color.red);
    }

    private void SetJumpAnimation()
    {
        playerVisuals.SetJumpAnimation(isGrounded);
    }
}
