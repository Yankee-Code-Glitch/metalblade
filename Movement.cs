using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float WalkSpeed = 1.0f;

    [SerializeField] float JumpForce = 1.0f;

    [SerializeField] float FallSpeed = 1.0f;
    
    private Rigidbody2D Rigidbody2D;

    private float Horizontal;

    private float Vertical;

    private int JumpCount = 0;

    private bool ifOnGround;

    private Animator MetalAnimator;

    // Start is called before the first frame update
    void Start()
    {
        MetalAnimator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        MetalAnimator.SetBool("Run", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 1.05f, Color.cyan);

        if (Physics2D.Raycast(transform.position, Vector3.down, 1.05f))
        {
            ifOnGround = true;
        }

        if (ifOnGround == true && JumpCount > 1)
        {
            JumpCount = 0;
        }

        else ifOnGround = false;

        Rigidbody2D.velocity = new Vector2(Horizontal * WalkSpeed, Rigidbody2D.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && JumpCount < 2)
        {
            Jump();
        }

        if (Rigidbody2D.velocity.y < 0)
        {
            Rigidbody2D.velocity += Vector2.up * Physics.gravity.y * FallSpeed * Time.deltaTime;
        }
        
    }

    //Update but fixed
    private void FixedUpdate()
    {
        
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);

        JumpCount = JumpCount + 1;
    }
}