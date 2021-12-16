using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    //public int maxHealth = 100;
    public int health;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        

        //gameObject.transform.position = Camera.main.WorldToScreenPoint([Reference to target].transform.position + posOffset);
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        SetHealth(maxHealth);
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
        this.health = health;
    }

    public void ChangeHealth(int amount)
    {
        SetHealth(health + amount);
    }

}
