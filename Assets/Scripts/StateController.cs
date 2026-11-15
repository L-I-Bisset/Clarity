using UnityEngine;

public class StateController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Ground Check Settings")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling Settings")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    bool exitingSlope;

    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    public State MovementState;
    public enum State
    {
        idle,
        sprinting,
        jumping,
    }

    private void Start()
    {
        MovementState = State.idle;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
        //Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        speedControl();

        //Handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
    
    }

    private void FixedUpdate()
    {
        switch (MovementState)
        {
            case State.idle:
                 idle();
                 break;
            case State.sprinting:
                movePlayer();
                break;
            case State.jumping:
                 readyToJump = false;
                 jump();
                 Invoke(nameof(ResetJump), jumpCooldown);
                 break;
        }
        
    }
    
    //sets movement state
    public void idle()
    {
        
        //sets movement state based on input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            MovementState = State.sprinting;
        }
        if (Input.GetButtonDown("Jump") && readyToJump && grounded)
        {
            MovementState = State.jumping;
        }
        
    }
    //Handles player movement
    private void movePlayer()
    {
        //Make player move in the direction theyre facing
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //Slope movement
        if (onSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.linearVelocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        //Ground movement
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        //Air movement
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        //Turn gravity off while on slope
        rb.useGravity = !onSlope();
        MovementState = State.idle;
    }

    //Controls the speed of the player
    private void speedControl()
    {
        //Makes movemnt on slop same as ground movement
        if (onSlope())
        {
            if(rb.linearVelocity.magnitude > moveSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;   
        }
        //Ground movement
        else{
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }
    }

    //Makes player jump
    private void jump()
    {
        //allows for jumping while on slope
        exitingSlope = true;
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        MovementState = State.idle;
    }

    //Resets the jump cooldown
    private void ResetJump()
    {
        readyToJump = true;
    }

    //Checks to see if player is on a slope
    private bool onSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}