using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    // Cached References
    Rigidbody2D myRigidBody;
    Animator playerAnimator;
    Collider2D playerCollider2D;

    // Methods
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Move();
        Jump();
        FlipSprite();
    }

    private void Move()
    {
        float controlInput = Input.GetAxis("Horizontal");
        myRigidBody.velocity = new Vector2(controlInput * moveSpeed, myRigidBody.velocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            playerAnimator.SetBool("IsRunning", true);
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
        }
    }

    private void Jump()
    {
        if (!playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void FlipSprite()
    {
        bool playerHasHoriztonalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        // if the player is moving horizontally
        if (playerHasHoriztonalSpeed)
        {
            // -ve makes the player sprite face left; +ve makes player sprite face right
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x),1f);
        }
    }
}
