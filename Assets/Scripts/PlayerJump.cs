using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTime;
    bool isJumping = false;
    float jumpTimeCounter;

    RaycastHit2D downHitRayCast;
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

        DrawBoxCastBounds();
    }

    private bool IsGrounded()
    {
        downHitRayCast = Physics2D.BoxCast(myColl.bounds.center, myColl.bounds.size, 0, Vector2.down, extraHeight, whatIsGround);

        if (downHitRayCast.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DrawBoxCastBounds()
    {
        Debug.DrawRay(myColl.bounds.center + new Vector3(myColl.bounds.extents.x, 0), Vector2.down * (myColl.bounds.extents.y + extraHeight), Color.red);
        Debug.DrawRay(myColl.bounds.center - new Vector3(myColl.bounds.extents.x, 0), Vector2.down * (myColl.bounds.extents.y + extraHeight), Color.red);
        Debug.DrawRay(myColl.bounds.center - new Vector3(myColl.bounds.extents.x, myColl.bounds.extents.y + extraHeight), Vector2.right * (myColl.bounds.extents.x * 2), Color.red);
    }
}
