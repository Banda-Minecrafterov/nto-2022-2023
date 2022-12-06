using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemTaker : InteractableDisableIfInteract
{
    [SerializeField]
    UInt32 itemId;
    [SerializeField]
    UInt32 itemCount;


    protected override void Interact()
    {
        if (InventoryMenu.RemoveItem(itemId, itemCount))
        {
            StartCoroutine(TipManager.ItemTake(itemId, itemCount));

            Disable();
        }
    }


    protected override SaveLoadManager.SaveObjectId GetSaveObjectId()
    {
        return SaveLoadManager.SaveObjectId.itemTaker0;
    }
}
