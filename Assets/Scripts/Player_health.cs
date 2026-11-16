using UnityEngine;

public class Player_health : MonoBehaviour
{
    public float  max_health = 100f;
    public float health = 0f;

    public void Start()
    {
        health = max_health;
    }


    public void takes_damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);

           gameOverScreen gameOverScreen = GameObject.Find("gameOverScreen").GetComponent<gameOverScreen>();
            gameOverScreen.activate();
        }
    }


}
