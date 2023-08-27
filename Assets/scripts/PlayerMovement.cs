using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    //variable used to hold the direction -1 to 1 of the player
    private float dirX = 0;

    //used to get the layer of the ground
    [SerializeField] private LayerMask jumpableGround;

    //Use for setting the movement speed
    [SerializeField] private float movementSpeed = 7f;

    //used for jump height
    [SerializeField] private float jumpForce = 14f;



    private enum MovementState {idle, running, jumping, falling};


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();


       
    }

    // Update is called once per frame
    private void Update()
    {

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);



        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {

        MovementState state;

        //checks to see if horizontal movement is great or less 0
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        // if it isn't it sets animation to idle
        else
        {
            state = MovementState.idle;
        }

        // to check for jumping see if velocity > 0
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }

        //if y is negative, player is falling
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
        
    }

    

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}


