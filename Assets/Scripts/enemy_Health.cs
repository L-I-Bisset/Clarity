using UnityEngine;

public class enemy_Health : MonoBehaviour
{
    public float health;
    public float maxHealth = 10f;
    //references the swingsword script from the swords object to check if sword is swinging
    public SwingSword swingSwordScript;
    
    public void Start()
    {
        health = maxHealth;
        swingSwordScript = GameObject.Find("Swords").GetComponent<SwingSword>();
        
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {
            // Check if the sword is swinging before applying damage
            if (swingSwordScript.isSwinging)
            {
                health -= collision.gameObject.GetComponent<DealingDamage>().damage;
            }
            
            if (health <= 0)
            {
                Destroy(gameObject);
            }

        }
    }

    
    public void takes_damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (gameObject.name == "Boss")
        {
           gameOverScreen gameOverScreen = GameObject.Find("gameOverScreen").GetComponent<gameOverScreen>();

           gameOverScreen.activate();
        }

    }



}
