// Script/movment.cs
using UnityEngine;
using System.Collections;
public class CameraController : MonoBehaviour
{
    public float mouseSensitivityX;
    public float mouseSensitivityY;
    
    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        //Locks cursor to the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        // Calculate rotation
        yRotation += mouseX;
        
        //clamps camera so camera cant flip over the head or under the feet
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //apply rotation to the camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
        
}