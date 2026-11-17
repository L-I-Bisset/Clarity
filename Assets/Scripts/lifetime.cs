using UnityEngine;

public class lifetime : MonoBehaviour
{
    public float lifetime_duration = 1f; 

    //lowers grenade life each frame
    void Update()
    {
        lifetime_duration -= Time.deltaTime;
        //if lifetime reaches 0 kill grenade
        if (lifetime_duration <= 0f)
        {
            Destroy(gameObject);
        }
    }


}
    