using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableItem : InventoryItemData
{
    public float speed;


    public override void Use(GameSlot item)
    {
        if (IsUsed())
        {
            item.slot.Remove(1);
            item.StartCoroutine(UseWait(item));
        }
    }


    IEnumerator UseWait(GameSlot item)
    {
        item.Block();
        float fillAmount = 1.0f;
        do
        {
            fillAmount -= Time.deltaTime * speed;
            item.BlockImage(fillAmount);
            yield return null;
        } while (fillAmount > 0.0f);
        item.Unblock();
    }


    protected abstract bool IsUsed();
}
