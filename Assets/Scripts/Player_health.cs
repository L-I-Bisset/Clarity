using UnityEngine;

public class Player_health : MonoBehaviour
{
    public float  max_health = 100f;
    public float health = 0f;
    public HealthBar healthBar;
    public void Start()
    {
        health = max_health;
        healthBar.setMaxHealth((int)max_health);
    }

    void Update()
    {
        Debug.Log("Player Health: " + health);
    }
    public void takes_damage(float damage)
    {
        health -= damage;
        healthBar.setHealth((int)health);

        if (health <= 0)
        {
            Destroy(gameObject);

           gameOverScreen gameOverScreen = GameObject.Find("gameOverScreen").GetComponent<gameOverScreen>();
            gameOverScreen.activate();
        }
    }


}
