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

    void Start()
    {
        //start countdown
        countdown = delay;
    }

  
    void Update()
    {
        //lower countdown
        countdown -= Time.deltaTime;

        //countdown ends and it hasnt exploded yet
        if (countdown <= 0f && !hasExploded)
        {
            //BOOM makes explosion effect
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

        //deal damage to enemies
        foreach (Collider nearbyObject in colliders)
        {
            enemy_Health explosion = nearbyObject.GetComponent<enemy_Health>();

            if (explosion != null)
            {
                explosion.takes_damage(damage);
            }
        }
        
        //makes grenade die
        Destroy(gameObject);
    }
}


