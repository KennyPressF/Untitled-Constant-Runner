using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    SpriteRenderer mySR;
    Animator myAnim;

    private void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
    }

    public void FlipSprite()
    {
        mySR.flipX = !mySR.flipX;
    }

    public void SetJumpAnimation(bool isGrounded)
    {
        myAnim.SetBool("isGrounded", isGrounded);
    }

    public void SetFallingAnimation(bool isFalling)
    {
        myAnim.SetBool("isFalling", isFalling);
    }
}
