using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Spawning : MonoBehaviour
{
    //variables
    [Header("Spawning")]
    public GameObject spawned;
    public int spawn_radius = 10;
    public float spawn_timer = 2f;
    private float timer;

    [Header("Invulnerability")]
    public float invunerability_radius = 20f;
    public bool invunerable = false;
    private bool enemyPresent = false;

    void Update()
    {
        //reset enemy present
        enemyPresent = false;


        // increase time until its higher than spawn timer
        timer += Time.deltaTime;

        if (timer > spawn_timer) {

            //if higher than spawn timer, spawn and reset timer
            spawn();
            timer = 0f;
        
        }

        //invun code

        //gets an array of objects in a radius around the boss
        Collider[] colliders = Physics.OverlapSphere(transform.position, invunerability_radius);

        //if theres an enemy in radius make him invulnerable
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy"))
            {
                invunerable = true;
                enemyPresent = true;

            }
        }

        // if there isnt an enemy present remove invulnerability
        if (!enemyPresent) {
            invunerable = false;
        }
    }

    void spawn()
    {
        //get a vector of a random place in a sphere of spawn radius
        Vector3 randomDirection = Random.insideUnitSphere * spawn_radius;

        //set the height to 3 to avoid as many enemies getting stuck as possible
        randomDirection.y = 3; 

        //get the spawn location as the random vector away from boss
        Vector3 spawnPosition = transform.position + randomDirection;

        //spawn enemy
        Instantiate(spawned, spawnPosition, Quaternion.identity);
    }
}
