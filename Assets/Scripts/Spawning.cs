using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Spawning : MonoBehaviour
{
    public GameObject spawned;
    public int spawn_radius = 10;
    public float spawn_timer = 2f;

    private float timer;

    [Header("Invulnerability")]
    public float invunerability_radius = 20f;
    public bool invunerable = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    

     
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawn_timer) {

            spawn();
            timer = 0f;
        
        }

        //invun code
        Collider[] colliders = Physics.OverlapSphere(transform.position, invunerability_radius);

        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy"))
            {
                invunerable = true;
                return;

            }
        }
    }

    void spawn()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawn_radius;
        randomDirection.y = 3; 
        Vector3 spawnPosition = transform.position + randomDirection;

        Instantiate(spawned, spawnPosition, Quaternion.identity);
    }
}
