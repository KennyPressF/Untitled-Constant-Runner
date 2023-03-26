using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Rigidbody2D myRB;
    
    RaycastHit2D forwardHitRayCast;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        myRB.velocity = new Vector2(moveSpeed * Time.fixedDeltaTime, myRB.velocity.y);
    }

    public void ChangeDirection()
    {
        moveSpeed = -moveSpeed;
    }
}

