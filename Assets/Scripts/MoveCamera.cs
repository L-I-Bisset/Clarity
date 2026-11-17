using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public Transform cameraPosition;

    void Update()
    {
        //Moves the camera to the players position
        transform.position = cameraPosition.position;
    }
}
