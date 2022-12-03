using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterHealth : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 100;
    int currentHealth;

    [SerializeField]
    HealthBar healthBar;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.MaxHealthPoint(maxHealth);
    }


    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            healthBar.HealthPoint(0);
            Die();
            return true;
        }
        else
        {
            healthBar.HealthPoint(currentHealth);
            return false;
        }
    }


    protected abstract void Die();
}
