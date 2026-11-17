
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;


public class enemy_ai : MonoBehaviour
{
    //variables
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask what_is_ground, What_is_player;

    public float gravity = -9.8f;

    public Rigidbody rigid;

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
    public Player_health player_Health;

    private void Awake()
    {
        //sets player, makes agent and gets rigidbody 
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();

        //reset attack to initally be false
        already_attacked = false;

    }

    private void Update()
    {
        //checks if its in  sight and if its in range of attack
        in_sight = Physics.CheckSphere(transform.position, sight_range, What_is_player);
        in_attack = Physics.CheckSphere(transform.position, attack_range, What_is_player);

        //if neither are true then it wanders around
        if (!in_sight && !in_attack){ 
            searching(); 
        }

        //if it cant attack but can see player chase
        if (in_sight && !in_attack){ 
            chasing(); 
        }

        // attack if it can
        if (in_sight && in_attack){ 
            attacking(); 
        }        
    }

    private void searching()
    {
        //if there isnt a walkpoint set make one
        if (!walk_point_set) 
        { 
            search_walk_point(); 
        }

        //go to walk point if there is
        else
        {
            agent.SetDestination(walk_point);
        }


            
        // get the distance from walk point
        Vector3 distance_to_walk = transform.position - walk_point;

        //if enemies close to walkpoint set false
        if ( distance_to_walk.magnitude < 1f)
        {
            walk_point_set = false;
        }
    }


    private void search_walk_point()
    {
        //gets a random x and z coordinate
        float rando_z = Random.Range(-walk_point_range, walk_point_range);
        float rando_x = Random.Range(-walk_point_range, walk_point_range);

        //make new walk point with current position plus random x and z away
        walk_point = new Vector3(transform.position.x + rando_x, transform.position.y, transform.position.z + rando_z);
       
        //if it can see walk point its valid and can be used
        if (Physics.Raycast(walk_point, -transform.up, 2f, what_is_ground))
        {
            walk_point_set = true;
        }
    }


    private void chasing()
    {
        //just walk towards player if it can
        agent.SetDestination(player.position);
    }


    private void attacking()
    {
        //makes it not move whilst attacking
        agent.SetDestination(transform.position);

        //Look at player before attacking.
        transform.LookAt(player);
    

        //access player health from player object
        player_Health = GameObject.Find("Player").GetComponent<Player_health>();

        //if players alive
        if (player_Health.health >= 0)
        {
            // if not attacked yet
            if (!already_attacked)
            {
                //deal damage
                player_Health.takes_damage(damage);
            }
        }   

        //resets attack
        if (!already_attacked)
        {
            already_attacked = true;
            Invoke(nameof(reset_attack), time_between_attacks);
        }
    }

    //just sets already attacked to false
    private void reset_attack()
    {
        already_attacked = false;

    }

    
}
