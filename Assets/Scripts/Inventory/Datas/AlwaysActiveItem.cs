using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysActiveItem : InventoryItemData
{
    public override bool StartChecking(GameSlot item)
    {
        return false;
    }
}
