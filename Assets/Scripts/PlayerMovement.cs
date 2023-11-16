using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Componets
    private Rigidbody2D rb;
    public SpriteRenderer playerSprite;
    private GameObject playerFoots; // For ground check
    private Animator playerAnimator;

    //Ground check Layer
    private LayerMask layer;

    //Move
    [Header("Movement")]
    public float moveSpeed = 5;
    //Wall Jump
    [Header("Wall Jump")]
    public float wallJumpTime = 0.2f;
    public float wallSlideSpeed = 0.3f;
    public float wallDistance = 0.53f;
    bool isWallSliding = false;
    RaycastHit2D WallCheckHit;
    float jumpTime;
    //Inputs
    private float vAxis = 0;
    private float hAxis = 0;
    private bool jumpInput = false;

    //Jump
    public float jumpForce = 1;
    private Vector3 jumpVector = Vector3.zero;


    private void Awake()
    {
        //Inicialization
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        playerFoots = GameObject.Find("PlayerFoots");
        layer = LayerMask.GetMask("Ground");
        playerAnimator = GetComponentInChildren<Animator>();
        
    }

    private void Update()
    {
        GetInputs();
        Move();

        CalculateJump();

        ChangeAnimations();

    
    }
    private void FixedUpdate()
    {
        Jump();

        FlipSprite();
        //Checks if the player is hanging to the wall 
        if (WallCheckHit && !IsGrounded() && hAxis != 0)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        }
        else if (jumpTime < Time.time) // after some time it stops the slide and falls
        {
            isWallSliding = false;
        }
        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlideSpeed, float.MaxValue));
        }



    }


    /// <summary>
    /// Read the Vertical and Horizontal Axis from Input
    /// </summary>
    private void GetInputs()
    {
        //Axis
        vAxis = Input.GetAxisRaw("Vertical");
        hAxis = Input.GetAxisRaw("Horizontal") * Time.timeScale;

        //Jump
        jumpInput = Input.GetKeyDown(KeyCode.Space);


    }
    /// <summary>
    /// Change Player position with axis and speed
    /// </summary>
    private void Move()
    {
        transform.position += Vector3.right * hAxis * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Change flip with horizontal move
    /// </summary>
    public void FlipSprite()
    {
        if (hAxis > 0)
        {
            playerSprite.flipX = false;
            //should keep the direction of shooting to the right
            //gameObject.GetComponent<Fireball>().ShootRight();
            //Stick to the wall on the right
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance , layer);
            Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.blue);

        }
        else if (hAxis < 0)
        {
            playerSprite.flipX = true;
            //should keep the direction of shooting to the right

            //gameObject.GetComponent<Fireball>().ShootLeft();

            //Stick to the wall on the left
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, layer);
            Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.blue);

        }

    }

    /// <summary>
    /// Check if the Player is on the ground
    /// </summary>
    /// <returns>is grounded</returns>
    private bool IsGrounded()
    {
        return Physics2D.Raycast(playerFoots.transform.position, Vector2.down, 0.5f, layer);
    }

    /// <summary>
    /// Calculate the Vector3 for the jump
    /// </summary>
    private void CalculateJump()
    {
        if (jumpInput && IsGrounded() || (isWallSliding && jumpInput))
        {
            jumpVector = Vector3.up * jumpForce;
            
        }
        else if (!IsGrounded())
            jumpVector = Vector3.zero;
    }

    /// <summary>
    /// Generate the Physics Jump movement
    /// </summary>
    private void Jump()
    {
        rb.AddForce(jumpVector, ForceMode2D.Impulse);
    }




    /// <summary>
    /// Change the Animation in order to action
    /// </summary>
    private void ChangeAnimations()
    {
        //Jumping
        if (!IsGrounded())
        {
           // Debug.Log("no pisa");
            playerAnimator.Play("PlayerJump");                              
        }
        //Running
        else if (IsGrounded() && hAxis != 0)
        {
            //Debug.Log(" pisa");

            playerAnimator.Play("PlayerRun");
        }
        //Idle
        else if (IsGrounded() && hAxis == 0)
        {
           // Debug.Log(" pisa");

            playerAnimator.Play("PlayerIdle");
        }
    }
}
