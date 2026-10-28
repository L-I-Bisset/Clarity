
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float smoothing = 1.5f;

    [Header("Vertical Clamp")]
    [SerializeField] private float minVerticalAngle = -90f;
    [SerializeField] private float maxVerticalAngle = 90f;

    private Transform playerBody;
    private float xRotation;
    private float yRotation;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    private void Awake()
    {
        playerBody = transform.parent;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
        }

        //float mouseY = Input.GetAxis("Mouse X");
        //float mouseX = Input.GetAxis("Mouse Y");

        //xRotation =- mouseY * mouseSensitivity;
        //xRotation = Mathf.Clamp(xRotation, -maxAngle, maxAngle);

        //yRotation = - mouseY * mouseSensitivity;

        //transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        Vector2 targetMouseDelta = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            Input.GetAxisRaw("Mouse Y")
        );

        currentMouseDelta = Vector2.SmoothDamp(
            currentMouseDelta, targetMouseDelta,
            ref currentMouseDeltaVelocity, 0f / smoothing
        );

        xRotation -= currentMouseDelta.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);

        yRotation += currentMouseDelta.x * mouseSensitivity;

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

}