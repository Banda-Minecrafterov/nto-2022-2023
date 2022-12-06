using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    public override bool TakeDamage(float damage)
    {
        bool isDead = base.TakeDamage(damage);
        InventoryManager.TakeDamage(isDead);
        return isDead;
    }

    public override bool RestoreHealth(int heal)
    {
        bool isFull = base.RestoreHealth(heal);
        InventoryManager.RestoreHealth(isFull || currentHealth == maxHealth);
        return isFull;
    }


    protected override void Die()
    {
        Debug.Log("Player dead");
    }
}
