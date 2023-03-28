using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideHitChecker : MonoBehaviour
{
    EdgeCollider2D wallColl;
    PlayerMovement playerMovement;

    private void Start()
    {
        wallColl = GetComponent<EdgeCollider2D>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeColliderSide();
        playerMovement.ChangeDirection();
    }

    public void ChangeColliderSide()
    {
        wallColl.offset = new Vector2(-wallColl.offset.x, 0);
    }
}
