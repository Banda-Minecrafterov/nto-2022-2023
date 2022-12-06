using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterHealth : MonoBehaviour
{
    [SerializeField]
    public float maxHealth     { get; private set; } = 100;
    public float currentHealth { get; private set; }

    [SerializeField]
    HealthBar healthBar;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.MaxHealthPoint(maxHealth);
    }


    public virtual bool TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            healthBar.HealthPoint(0);
            Die();
            return true;
        }

        healthBar.HealthPoint(currentHealth);
        return false;
    }

    public virtual bool RestoreHealth(int heal)
    {
        if (currentHealth == maxHealth)
            return false;

        currentHealth += heal;

        if (currentHealth > maxHealth)
            currentHealth = heal;

        healthBar.HealthPoint(currentHealth);
        return true;
    }


    protected abstract void Die();
}
