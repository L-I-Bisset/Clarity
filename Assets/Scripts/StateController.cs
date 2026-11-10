using UnityEngine;

public class StateController : MonoBehaviour
{
    

    //Enum to hold the different states of the player
    public enum State
    {
        Idle,
        Dashing,
        Running,
        Jumping,
        Attacking,
        Sliding
    }
    [SerializeField] public float walkSpeed = 6f;
    [SerializeField] public float gravity = -9.8f;
    [SerializeField] public float mouseSensitivity = 2.5f;
    public State currentState;
    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private float currentSpeed;
    private Vector3 velocity;
    private CharacterController controller;
    private Camera playerCamera;
    private float rotationX = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        currentState = State.Idle;
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        rb = GetComponent<Rigidbody>();

        if (playerCamera == null)
        {
            Debug.LogError("Main camera not found. Please assign a camera tagged as 'MainCamera'.");
        }

        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor for first-person view
        Cursor.visible = false;  // Hide the cursor
    }

    // Update is called once per frame
    private void Update()
    {
        cameraRotation();
    }

    private void FixedUpdate()
    {
        //applyGravity();
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 2f);

        switch (currentState)
        {
            case State.Idle:
                updateIdle();
                break;
            case State.Running:
                updateRunning();
                break;
            case State.Dashing:
                updateDashing();
                break;
            case State.Jumping:
                updateJumping();
                break;
            case State.Attacking:
                updateAttacking();
                break;
            case State.Sliding:
                updateSliding();
                break;
        }
    }

    private void updateIdle()
    {
        float move = Input.GetAxis("Horizontal");

        if (move != 0)
        {
            currentState = State.Running;
        }
        else if (Input.GetButtonDown("Jump") && isGrounded)
        {
            currentState = State.Jumping;
        }
        else if (Input.GetButtonDown("Dash") && isGrounded)
        {
            currentState = State.Dashing;
        }
        else if (Input.GetButtonDown("Fire1") && isGrounded)
        {
            currentState = State.Attacking;
        }
        else if (Input.GetButtonDown("Slide") && isGrounded)
        {
            currentState = State.Sliding;
        }
    }
    private void updateRunning()
    {
        
        //float moveDirectionY = velocity.y;
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionZ = Input.GetAxis("Vertical");

        currentSpeed = walkSpeed; // Set the current speed to walk speed

        Vector3 move = new Vector3(moveDirectionX, 0, moveDirectionZ);
        rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);

        // Apply movement and velocity
        velocity = move * currentSpeed;

        //velocity.y = moveDirectionY;  // Apply gravity
        controller.Move(velocity * Time.deltaTime);
        currentState = State.Idle; // Reset to Idle state after processing movement
    }

    private void updateDashing()
    {
        currentState = State.Idle;
    }

    private void updateJumping()
    {
        if (isGrounded)
        {

        }
        currentState = State.Idle;
    }

    private void updateAttacking()
    {
        currentState = State.Idle;
    }

    private void updateSliding()
    {
        if (isGrounded)
        {
            
        }
        currentState = State.Idle;
    }

    private void applyGravity()
    {
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }
    private void cameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Vertical camera rotation limits

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); // Apply vertical rotation
        transform.Rotate(Vector3.up * mouseX); // Apply horizontal rotation
    }
}
