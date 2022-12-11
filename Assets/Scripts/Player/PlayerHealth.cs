using System.IO;
using UnityEngine;

public class PlayerHealth : CharacterHealth, ISaveLoadData
{
    new void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.PlayerHealth, this);

        base.Awake();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            TakeDamage(10000);
    }


    public override bool TakeDamage(float damage)
    {
        bool isDead = base.TakeDamage(damage);
        InventoryManager.TakeDamage(isDead);

        if (isDead)
        {
            Debug.Log(character.animator.gameObject.name);
            character.animator.SetBool("Die", true);
            character.enabled = false;
        }
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


    public void Save(BinaryWriter data)
    {
        data.Write(currentHealth);
    }

    public void Load(BinaryReader data, int version)
    {
        currentHealth = data.ReadInt32();

        Init();
        healthBar.SetValue(currentHealth);
    }
}
