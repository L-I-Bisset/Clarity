using UnityEngine;

public class enemy_Damage : MonoBehaviour
{
    public float health = 10f;
    public float damage = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void takes_damage(float damage)
    {
        health -= damage;

        if (health <= 0) { Destroy(this, 0f); }
    }

}
