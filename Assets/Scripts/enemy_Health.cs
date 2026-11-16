using UnityEngine;

public class enemy_Health : MonoBehaviour
{
    public float health;
    public float maxHealth = 10f;

    public void Start()
    {
        health = maxHealth;
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {

            health -= collision.gameObject.GetComponent<DealingDamage>().damage;
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
