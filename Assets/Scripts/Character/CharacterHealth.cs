using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Character))]
public abstract class CharacterHealth : MonoBehaviour
{
    [SerializeField]
    protected Bar healthBar;

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
        healthBar.SetMax(character.maxHealth);
    }


    public virtual bool TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            healthBar.SetValue(0);
            return true;
        }

        healthBar.SetValue(currentHealth);
        return false;
    }

    public virtual bool RestoreHealth(int heal)
    {
        if (currentHealth == character.maxHealth)
            return false;

        currentHealth += heal;

        if (currentHealth > character.maxHealth)
            currentHealth = heal;

        healthBar.SetValue(currentHealth);
        return true;
    }
}
