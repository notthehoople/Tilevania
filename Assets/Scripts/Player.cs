using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] float moveSpeed = 5f;

    // Cached References
    Rigidbody2D myRigidBody;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        FlipSprite();
    }

    private void Move()
    {
        // TODO: Changing transform.position to use RigidBody2D.velocity for now. Might swap this back later
        //var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        //transform.position = new Vector2(transform.position.x + deltaX, transform.position.y);

        // TODO: RigidBody2D.velocity method of moving. When you let go of the key the player still moves
        float controlInput = Input.GetAxis("Horizontal");
        myRigidBody.velocity = new Vector2(controlInput * moveSpeed, myRigidBody.velocity.y);
    }

    private void FlipSprite()
    {
        bool playerHasHoriztonalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        // if the player is moving horizontally
        if (playerHasHoriztonalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x),1f);
        }
            // reverse the current scaling of x axis
    }
}
