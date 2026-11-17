using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public void setMaxHealth(int health)
    {
        //set slider to be full
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    public void setHealth(int health)
    {
        //lower healthbar slider
        healthSlider.value = health;
    }
}
