using System.Collections;
using UnityEngine;

public class SwingSword : MonoBehaviour
{
    private bool isSwinging = false;
    private float swingDuration = 0.2f; 
    public Transform holdPosition;

    void Update()
    {

        if (!isSwinging)
        {
            // Keep the sword at the hold position when not swinging
            transform.position = holdPosition.position;
            transform.rotation = holdPosition.rotation * Quaternion.Euler(-90, -30, 0);
        }
        if (Input.GetButtonDown("Fire1") && !isSwinging)
        {
            swingSword();
        }
    }

    private void swingSword()
    {
        if (!isSwinging)
        {
            isSwinging = true;
            StartCoroutine(SwingCoroutine());
        }
    }
    
    IEnumerator SwingCoroutine()
    {
        //Rotate sword around location of sword hilt object for duration of swing
        float elapsedTime = 0f;
        while (elapsedTime < swingDuration)
        {
            transform.Rotate(0, 0, -140 * Time.deltaTime / swingDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset the sword rotation after the swing
        transform.Rotate(0, 0, 140); 
        isSwinging = false;
    }
}
