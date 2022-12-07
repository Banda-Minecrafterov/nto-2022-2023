using System.IO;
using UnityEngine;

public class PlayerHealth : CharacterHealth, ISaveLoadData
{
    new void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.PlayerHealth, this);

        base.Awake();
    }


    public override bool TakeDamage(float damage)
    {
        bool isDead = base.TakeDamage(damage);
        InventoryManager.TakeDamage(isDead);

        if (isDead)
            Debug.Log("Player dead");
        return isDead;
    }

    public override bool RestoreHealth(int heal)
    {
        if (base.RestoreHealth(heal))
        {
            InventoryManager.RestoreHealth(currentHealth == character.maxHealth);
            return true;
        }
        return false;
    }


    public void Save(ref BinaryWriter data)
    {
        data.Write(currentHealth);
    }

    public void Load(ref BinaryReader data, int version)
    {
        currentHealth = data.ReadInt32();

        Init();
        healthBar.HealthPoint(currentHealth);
    }
}
