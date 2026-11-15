using UnityEngine;

public class lifetime : MonoBehaviour
{
    public float lifetime_duration = 1f; 

    void Start()
    {
        Invoke(nameof(Destroy), lifetime_duration);
    }

   
}
