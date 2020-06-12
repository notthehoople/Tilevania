using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] float moveSpeed = 5f;

    // Cached References
    Rigidbody2D myRigidBody;
    Animator playerAnimator;

    // Methods
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
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
