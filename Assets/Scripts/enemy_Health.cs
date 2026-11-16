using UnityEngine;

public class enemy_Health : MonoBehaviour
{
    public float health = 10f;
    

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
            GameObject.Find("game over screen").GetComponent<gameOverScreen>().setup();
        }

    }



}
