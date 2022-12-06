using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemGetter : InteractableDisableIfInteract
{
    [SerializeField]
    UInt32 itemId;
    [SerializeField]
    UInt32 itemCount;


    protected override void Interact()
    {
        if (InventoryMenu.AddItem(itemId, itemCount))
        {
            StartCoroutine(TipManager.ItemGet(itemId, itemCount));

            Disable();
        }
    }


    protected override SaveLoadManager.SaveObjectId GetSaveObjectId()
    {
        return SaveLoadManager.SaveObjectId.itemGetter0;
    }
}
