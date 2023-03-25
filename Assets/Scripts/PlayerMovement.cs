using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTime;
    bool isJumping = false;
    bool ifFalling = false;
    float jumpTimeCounter;

    RaycastHit2D groundHitRayCast;
    [SerializeField] float extraHeight;
    [SerializeField] LayerMask whatIsGround;

    Rigidbody2D myRB;
    BoxCollider2D myColl;
    PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myColl = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void Update()
    {
        Jump();

        if (transform.position.x < -16)
        {
            moveSpeed = 400;
        }
        if (transform.position.x > 16)
        {
            moveSpeed = -moveSpeed;
        }
    }

    private void FixedUpdate()
    {
        myRB.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, myRB.velocity.y);
    }

    private void Jump()
    {
        if (playerInputActions.Player.Jump.WasPressedThisFrame() && IsGrounded())
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);
            //myRB.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (playerInputActions.Player.Jump.IsPressed())
        {
            if (jumpTimeCounter > 0 && isJumping)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);
                //myRB.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (playerInputActions.Player.Jump.WasReleasedThisFrame())
        {
            isJumping = false;
        }
    }

    private bool IsGrounded()
    {
        groundHitRayCast = Physics2D.BoxCast(myColl.bounds.center, myColl.bounds.size, 0, Vector2.down, extraHeight, whatIsGround);

        if (groundHitRayCast.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

