using System;
using UnityEngine;

public class ItemGetter : SaveableInteractable
{
    [SerializeField]
    UInt32 itemId;
    [SerializeField]
    UInt32 itemCount;


    protected override void Interact()
    {
        if (InventoryManager.AddItem(itemId, itemCount))
        {
            StartCoroutine(TipManager.ItemGet(itemId, itemCount));

            Disable();
        }
    }
}
