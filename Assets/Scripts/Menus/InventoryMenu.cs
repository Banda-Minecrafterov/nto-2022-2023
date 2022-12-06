using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryMenu : BaseMenu
{
    [SerializeField]
    Transform inventorySlotsParent;
    [SerializeField]
    Transform handSlotsParent;

    InventorySlot[] slots;

    public static InventoryMenu menu { get; private set; }


    void Awake()
    {
        menu = this;

        slots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>().Concat(handSlotsParent.GetComponentsInChildren<InventorySlot>()).ToArray();

        gameObject.SetActive(false);
    }

    
    public static bool IsContains(UInt32 id)
    {
        foreach(var slot in menu.slots)
        {
            if (slot.id == id) return true;
        }
        return false;
    }

    public static bool AddItem(UInt32 id, UInt32 count)
    {
        foreach (var slot in menu.slots)
        {
            if (slot.id == id)
            {
                return slot.Add(count);
            }
        }
        foreach (var slot in menu.slots)
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
        foreach (var slot in menu.slots)
        {
            if (slot.id == id)
            {
                return slot.Remove(count);
            }
        }
        return false;
    }
}
