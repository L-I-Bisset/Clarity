
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;


public class enemy_ai : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask what_is_ground, What_is_player;

    public float gravity = -9.8f;

    public Rigidbody rigid;

    public float health = 10f;

    public float damage = 1f;


        

    //searching
    public Vector3 walk_point;
    bool walk_point_set;
    public float walk_point_range;

    //attacking
    public float time_between_attacks;
    bool already_attacked;

    //states
    public float sight_range, attack_range;
    public bool in_sight, in_attack;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        rigid = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        in_sight = Physics.CheckSphere(transform.position, sight_range, What_is_player);
        in_attack = Physics.CheckSphere(transform.position, attack_range, What_is_player);

        if (!in_sight && !in_attack){ 
            searching(); 
        }

        if (in_sight && !in_attack){ 
            chasing(); 
        }

        if (in_sight && in_attack){ 
            attacking(); 
        }
        
    }

    private void searching()
    {
        if (!walk_point_set) { search_walk_point(); }


        if (walk_point_set) {
            agent.SetDestination(walk_point);

        }

        Vector3 distance_to_walk = transform.position - walk_point;

        if ( distance_to_walk.magnitude < 1f)
        {
            walk_point_set = false;
        }
    }


    private void search_walk_point()
    {
        float rando_z = Random.Range(-walk_point_range, walk_point_range);
        float rando_x = Random.Range(-walk_point_range, walk_point_range);

        walk_point = new Vector3(transform.position.x + rando_x, transform.position.y, transform.position.z + rando_z);
       

        if (Physics.Raycast(walk_point, -transform.up, 2f, what_is_ground))
        {
            walk_point_set = true;
        }
    }


    private void chasing()
    {
        agent.SetDestination(player.position);
    }

    private void attacking()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

       // player.GetComponent<takes_damage(damage)>();   
       

        if (!already_attacked)
        {
            already_attacked = true;
            Invoke(nameof(reset_attack), time_between_attacks);
        }
    }

    private void reset_attack()
    {
        already_attacked = false;

    }

    void takes_damage(float damage)
    {
        health -= damage;

        if (health <= 0) { Destroy(this, 0f); }
    }
}
