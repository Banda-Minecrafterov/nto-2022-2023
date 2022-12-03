using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    private int maxHealth = 100;
    public int currentHealth;

    public EnemyHealthBar healthBar;

    private Player playerRoll;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.MaxHealthPoint(maxHealth);
        playerRoll = GetComponent<Player>();
        
    }
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

       healthBar.HealthPoint(currentHealth);
        

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy died!");

        Destroy(gameObject);
    }
}
