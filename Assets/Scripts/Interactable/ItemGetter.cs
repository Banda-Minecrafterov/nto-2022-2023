using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetter : Interactable
{
    [SerializeField]
    UInt32 itemId;
    [SerializeField]
    UInt32 itemCount;


    protected override void Interact()
    {
        if (InventoryMenu.AddItem(itemId, itemCount))
        {
            Destroy(gameObject);
        }
    }
}
