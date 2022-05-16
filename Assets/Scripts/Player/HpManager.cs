using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    public Slider slider;

    public int maxHealth = 100;
    public int currentHealth;
    //player
    public int playerHp = 100;
    public int damagePlayer = 20;
    //zombies
    private void Start()
    {
        //when start, set health to maxhealth
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        //if Attack, TakeDmage(value of damage)
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SetHealth(currentHealth);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
