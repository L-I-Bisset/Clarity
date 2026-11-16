using System.Collections;
using UnityEngine;

public class SwingSword : MonoBehaviour
{
    public bool isSwinging = false;
    private float swingDuration = 0.2f; 
    public Transform holdPosition;

    void Update()
    {

        if (!isSwinging)
        {
            // Keep the sword at the hold position and rotate it smoothly to the hold rotation
        
            transform.rotation = Quaternion.Lerp(transform.rotation, holdPosition.rotation * Quaternion.Euler(-90, -32, 0), Time.deltaTime * 12);
            transform.position = holdPosition.position + new Vector3(0, 0.25f, 0);
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
        //Rotate sword around rotation center for duration of swing
        float elapsedTime = 0f;
        while (elapsedTime < swingDuration)
        {
            //Keep sword on the player during the swing animation
            transform.position = holdPosition.position + new Vector3(0, 0.25f, 0);
            transform.Rotate(0, 0, -140 * Time.deltaTime / swingDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset the sword rotation after the swing
        transform.Rotate(0, 0, 140); 
        isSwinging = false;
    }
}
