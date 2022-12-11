using System;
using UnityEngine;

public class ItemTaker : SaveableInteractable
{
    [SerializeField]
    UInt32 itemId;
    [SerializeField]
    UInt32 itemCount;


    protected override void Interact()
    {
        if (InventoryManager.RemoveItem(itemId, itemCount))
        {
            StartCoroutine(TipManager.ItemTake(itemId, itemCount));

            Disable();
        }
    }
}
