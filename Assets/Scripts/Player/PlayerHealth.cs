using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public bool gameOver = false;

    public HealthBar healthBar;


    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.MaxHealthPoint(maxHealth);
    }

    private void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

        healthBar.HealthPoint(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Player died");
            gameOver = true;

            GetComponent<Collider2D>().enabled = false;

        }
    }
}
