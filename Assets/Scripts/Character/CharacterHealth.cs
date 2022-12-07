using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Character))]
public abstract class CharacterHealth : MonoBehaviour
{
    [SerializeField]
    protected HealthBar healthBar;

    protected Character character { get; private set; }

    public float currentHealth { get; protected set; }


    protected void Awake()
    {
        character = GetComponent<Character>();
        currentHealth = character.maxHealth;
    }

    void Start()
    {
        Init();
    }


    protected void Init()
    {
        healthBar.MaxHealthPoint(character.maxHealth);
    }


    public virtual bool TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            healthBar.HealthPoint(0);
            return true;
        }

        healthBar.HealthPoint(currentHealth);
        return false;
    }

    public virtual bool RestoreHealth(int heal)
    {
        if (currentHealth == character.maxHealth)
            return false;

        currentHealth += heal;

        if (currentHealth > character.maxHealth)
            currentHealth = heal;

        healthBar.HealthPoint(currentHealth);
        return true;
    }
}
