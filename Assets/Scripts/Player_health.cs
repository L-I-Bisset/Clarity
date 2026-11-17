using UnityEngine;

public class Player_health : MonoBehaviour
{
    public float  max_health = 100f;
    public float health = 0f;
    public HealthBar healthBar;
    public gameOverScreen gameOver;

    public void Start()
    {
        //initalising health to max health and setting the max health of healthbar
        health = max_health;
        healthBar.setMaxHealth((int)max_health);
    }

  
    public void takes_damage(float damage)
    {
        //health lost on player and healthbar
        health -= damage;
        healthBar.setHealth((int)health);

        if (health <= 0)
        {
            //if no health left destroy player and call game over screen to start
            Destroy(gameObject);

           
            gameOver.activate();
        }
    }


}
