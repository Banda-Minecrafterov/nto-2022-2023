using System;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    Transform gameSlots;

    [SerializeField]
    InventoryItemData[] datas;

    [SerializeField]
    Transform inventorySlotsParent;
    [SerializeField]
    Transform handSlotsParent;

    InventorySlot[] slots;

    public static InventoryManager manager { get; private set; }

    public static InventoryItemData[] items
    {
        get
        {
            return manager.datas;
        }
    }


    void Awake()
    {
        manager = this;

        slots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>().Concat(handSlotsParent.GetComponentsInChildren<InventorySlot>()).ToArray();
    }


    public static bool IsContains(UInt32 id)
    {
        foreach (var slot in manager.slots)
        {
            if (slot.id == id) return true;
        }
        return false;
    }

    public static bool AddItem(UInt32 id, UInt32 count)
    {
        foreach (var slot in manager.slots)
        {
            if (slot.id == id)
            {
                return slot.Add(count);
            }
        }
        foreach (var slot in manager.slots)
        {
            if (slot.id == InventorySlot.NoId)
            {
                slot.Init(id, count);
                return true;
            }
        }
        return false;
    }

    public static bool RemoveItem(UInt32 id, UInt32 count)
    {
        foreach (var slot in manager.slots)
        {
            if (slot.id == id)
            {
                return slot.Remove(count);
            }
        }
        return false;
    }


    public static Transform GetGameSlot(int count)
    {
        return manager.gameSlots.GetChild(count);
    }


    public static void TakeDamage(bool isDead)
    {
        foreach (Transform i in manager.gameSlots)
        {
            i.GetComponent<GameSlot>().TakeDamage(isDead);
        }
    }

    public static void RestoreHealth(bool isFull)
    {
        foreach (Transform i in manager.gameSlots)
        {
            i.GetComponent<GameSlot>().RestoreHealth(isFull);
        }
    }
}
