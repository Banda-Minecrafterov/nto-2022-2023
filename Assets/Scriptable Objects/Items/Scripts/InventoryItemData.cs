using UnityEngine;

public abstract class InventoryItemData : ScriptableObject
{
    public string displayName;
    [TextArea]
    public string description;

    public GameObject icon;


    public virtual bool StartChecking(GameSlot item) { return true; }
    public virtual void End(GameSlot item) {  }

    public virtual void TakeDamage(GameSlot item, bool isDead) { }
    public virtual void RestoreHealth(GameSlot item, bool isFull) { }

    public virtual void Use(GameSlot item) { }
}
