using UnityEngine;

public class Player_health : MonoBehaviour
{
    public float  max_health = 100f;
    public float health = 0f;

    public void takes_damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);

            GameObject.Find("game over screen").GetComponent<gameOverScreen>().setup();
        }
    }


}
