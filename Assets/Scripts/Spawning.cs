using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Spawning : MonoBehaviour
{
    public GameObject spawned;
    public int spawn_radius = 10;
    public float spawn_timer = 2f;

    private float timer;

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

    }

    void spawn()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawn_radius;
        randomDirection.y = 0; 
        Vector3 spawnPosition = transform.position + randomDirection;

        Instantiate(spawned, spawnPosition, Quaternion.identity);
    }
}
