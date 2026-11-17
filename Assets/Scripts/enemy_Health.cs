using UnityEngine;

public class enemy_Health : MonoBehaviour
{
    public float health;
    public float maxHealth = 10f;
    //references the swingsword script from the swords object to check if sword is swinging
    public SwingSword swingSwordScript;
    public gameOverScreen gameOver;

    public void Start()
    {
        //settinghealth and finding sword
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
                //deal damage from object
                health -= collision.gameObject.GetComponent<DealingDamage>().damage;
            }
          
        }
    }

    
    public void takes_damage(float damage)
    {
        health -= damage;

        //if healths low destorys object
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        //if boss dies activate game over screen
        if (gameObject.tag == "boss")
        {
            gameOver.activate();
        }

    }



}
