using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isJumping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if the character is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Get the user input for movement
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Move the character horizontally
        Vector2 movement = new Vector2(moveHorizontal * movementSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Jump when grounded and the jump key is pressed
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        // Handle mid-air jumps
        if (isJumping && rb.velocity.y <= 0)
        {
            rb.gravityScale = 1f;
            isJumping = false;
        }
        else if (!isJumping && rb.velocity.y > 0)
        {
            rb.gravityScale = 0.5f;
        }
    }
}