using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Movement settings
    [SerializeField] public float walkSpeed = 6f;
    [SerializeField] public float gravity = -9.8f;
    [SerializeField] public float mouseSensitivity = 2f;

    // Private variables
    private float currentSpeed;
    private Vector3 velocity;
    private CharacterController controller;
    private Camera playerCamera;
    private float rotationX = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main;

        if (playerCamera == null)
        {
            Debug.LogError("Main camera not found. Please assign a camera tagged as 'MainCamera'.");
        }

        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor for first-person view
        Cursor.visible = false;  // Hide the cursor
    }

    private void Update()
    {
        if (playerCamera == null) return; // Exit if no camera is assigned

        HandleMovement();
        HandleCameraRotation();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        float moveDirectionY = velocity.y;
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionZ = Input.GetAxis("Vertical");

        currentSpeed = walkSpeed; // Set the current speed to walk speed

        Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionZ;

        // Apply movement and velocity
        velocity = move * currentSpeed;

        velocity.y = moveDirectionY;  // Apply gravity
        controller.Move(velocity * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }

    private void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Vertical camera rotation limits

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); // Apply vertical rotation
        transform.Rotate(Vector3.up * mouseX); // Apply horizontal rotation
    }
}
