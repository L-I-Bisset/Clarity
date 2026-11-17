using UnityEngine;
using UnityEngine.UI;

public class GrenadeCooldown : MonoBehaviour
{
    public Image cooldownImage;
    public PlayerController playerController;
    public float cooldownTimer;

    void Start()
    {
        cooldownTimer = 0;
    }
    // Update is called once per frame
    public void Update()
    {   
        //reduces cooldown timer by time, and changes the fill amount of the cooldown image
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownImage.fillAmount = cooldownTimer / playerController.Cooldown;
        }
    }

}
