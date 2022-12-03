using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTaker : Interactable
{
    [SerializeField]
    UInt32 id;
    [SerializeField]
    UInt32 count;


    protected override void Interact()
    {
        if (InventoryMenu.RemoveItem(id, count))
        {
            Destroy(gameObject);
        }
    }
}
