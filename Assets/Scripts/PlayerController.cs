using UnityEngine;

public class PlayerController : MonoBehaviour
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
  

    [Header("Grenade Settings")]
    public GameObject grenadePrefab;
    public float ThrowForce = 10f;
    public float UpwardsThrowForce = 10f;
    public float Cooldown = 2f;
    public KeyCode throwKey = KeyCode.Mouse1;
    public Transform cam;
    public Transform throwPoint;
    bool readyToThrow = true;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;


        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
        //Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        speedControl();

        //Handle drag
        if (grounded) {
            rb.linearDamping = groundDrag;
        }
        else { 
                rb.linearDamping = 0; 
        }

        if(Input.GetKeyDown(throwKey) && readyToThrow)
        {
            ThrowGrenade();
        }
    }

    private void ThrowGrenade()
    {
        readyToThrow = false;
        //Instantiate grenade
        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody grenadeRb = grenade.GetComponent<Rigidbody>();
        grenadeRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        grenadeRb.interpolation = RigidbodyInterpolation.Interpolate;

        //Calculate direction to throw

        Vector3 forceDirection = cam.transform.forward * ThrowForce + transform.up * UpwardsThrowForce;
      
        grenadeRb.AddForce(forceDirection, ForceMode.Impulse);

        //Cooldown
        Invoke(nameof(ResetThrow), Cooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    //Handles player input
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    //Handles player movement
    private void MovePlayer()
    {
        //Make player move in the direction theyre facing
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //Slope movement
        if (OnSlope() && !exitingSlope)
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
        rb.useGravity = !OnSlope();
    }

    //Controls the speed of the player
    private void speedControl()
    {
        //Makes movemnt on slop same as ground movement
        if (OnSlope())
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
    private void Jump()
    {
        //allows for jumping while on slope
        exitingSlope = true;
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    //Resets the jump cooldown
    private void ResetJump()
    {
        readyToJump = true;
    }

    //Checks to see if player is on a slope
    private bool OnSlope()
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