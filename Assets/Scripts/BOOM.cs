using System.Runtime.CompilerServices;
using UnityEngine;

public class BOOM : MonoBehaviour
{
    public float delay = 3f;
    float countdown;
    bool hasExploded = false;
    public GameObject explosionEffect;
    public float explosionradius = 5f;
    public float damage = 25f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        //Show effect
        Instantiate(explosionEffect, transform.position, transform.rotation);

        //Detect nearby objects
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionradius);

        foreach (Collider nearbyObject in colliders)
        {
            enemy_Health explosion = nearbyObject.GetComponent<enemy_Health>();

            if (explosion != null)
            {
                explosion.takes_damage(damage);
            }
        }
            
        Destroy(gameObject);
    }
}


