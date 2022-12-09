using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameSlot : ItemSlot
{
    [SerializeField]
    public HandSlot slot;

    Image blockImage;

    Coroutine keyCheck;


    new void Awake()
    {
        blockImage = transform.GetChild(1).GetComponent<Image>();

        base.Awake();
    }


    public bool Unblock()
    {
        if (InventoryManager.items[slot.id].StartChecking(this))
        {
            keyCheck = StartCoroutine(KeyCheck(KeyCode.Alpha1 + transform.GetSiblingIndex()));
            return true;
        }
        return false;
    }

    public bool Block()
    {
        try
        {
            StopCoroutine(keyCheck);
        }
        catch { return false; }
        return true;
    }

    public void BlockImage(float fillAmount)
    {
        blockImage.fillAmount = fillAmount;
    }


    public void TakeDamage(bool isDead)
    {
        if (slot.id != InventorySlot.NoId)
        {
            InventoryManager.items[slot.id].TakeDamage(this, isDead);
        }
    }

    public void RestoreHealth(bool isFull)
    {
        if (slot.id != InventorySlot.NoId)
        {
            InventoryManager.items[slot.id].RestoreHealth(this, isFull);
        }
    }


    public new void Add(UInt32 newCount)
    {
        base.Add(newCount);
    }

    public new void Remove(UInt32 newCount)
    {
        base.Remove(newCount);
    }


    public new void Init(UInt32 id, UInt32 count)
    {
        base.Init(id, count);
        Unblock();
    }


    public new void ItemLost()
    {
        base.ItemLost();

        Remove(0);
        Block();
    }


    IEnumerator KeyCheck(KeyCode key)
    {
        while (true)
        {
            if (!PauseMenu.isPaused && Input.GetKeyDown(key))
            {
                InventoryManager.items[slot.id].Use(this);
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
    }
}
