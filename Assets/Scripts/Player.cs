using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    bool isAlive = true;

    // Cached References
    Rigidbody2D playerRigidBody;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider2D;
    BoxCollider2D playerFeetCollider2D;
    float gravityScaleAtStart;

    // Methods
    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider2D = GetComponent<CapsuleCollider2D>();
        playerFeetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = playerRigidBody.gravityScale;
    }

    private void Update()
    {
        if (!isAlive) { return; }

        Move();
        Jump();
        Climb();
        FlipSprite();
        Die();
    }

    private void Move()
    {
        float controlInput = Input.GetAxis("Horizontal");
        playerRigidBody.velocity = new Vector2(controlInput * moveSpeed, playerRigidBody.velocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (!playerFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            playerRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void Climb()
    {
        if (!playerFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            playerAnimator.SetBool("IsClimbing", false);
            playerRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        float verticalInput = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(playerRigidBody.velocity.x, verticalInput * climbSpeed);
        playerRigidBody.velocity = climbVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("IsClimbing", playerHasVerticalSpeed);
        playerRigidBody.gravityScale = 0;
    }

    private void FlipSprite()
    {
        bool playerHasHoriztonalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;

        // if the player is moving horizontally
        if (playerHasHoriztonalSpeed)
        {
            // -ve makes the player sprite face left; +ve makes player sprite face right
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x),1f);
        }
    }

    private void Die()
    {
        if (playerBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            playerAnimator.SetTrigger("Dying");
            playerRigidBody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
