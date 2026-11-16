using UnityEngine;

public class lifetime : MonoBehaviour
{
    public float lifetime_duration = 1f; 

    void Update()
    {
        lifetime_duration -= Time.deltaTime;
        if (lifetime_duration <= 0f)
        {
            Destroy(gameObject);
        }
    }


}
    