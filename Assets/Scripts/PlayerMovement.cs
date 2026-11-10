using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Movement settings
    [SerializeField] public float walkSpeed = 6f;
    [SerializeField] public float gravity = -9.8f;
    [SerializeField] public float mouseSensitivity = 2.5f;

    private float currentSpeed;
    private Vector3 velocity;
    private CharacterController controller;
    private Camera playerCamera;
    private float rotationX = 0f;
    private Rigidbody rb;

    private void Start()
    {
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

    private void Update()
    {
        cameraRotation();
    }

    private void FixedUpdate()
    {
        //Fixed updates for physics
        movement();
        applyGravity();

    }

    private void movement()
    {
        float moveDirectionY = velocity.y;
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionZ = Input.GetAxis("Vertical");

        currentSpeed = walkSpeed; // Set the current speed to walk speed

        Vector3 move = new Vector3(moveDirectionX, 0, moveDirectionZ);
        rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);

        // Apply movement and velocity
        velocity = move * currentSpeed;

        velocity.y = moveDirectionY;  // Apply gravity
        controller.Move(velocity * Time.deltaTime);
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
