using UnityEngine;

public class Player_health : MonoBehaviour
{
    public float  max_health = 100f;
    public float health = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = max_health;
    }   
        
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takes_damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this, 0f);
        }
    }


}
