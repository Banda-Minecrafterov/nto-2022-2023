using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : Graphic
{
    protected GameObject countText;
    protected Transform itemTransform;


    protected override void Awake()
    {
        countText = transform.Find("Count").gameObject;

        base.Awake();

#if DEBUG
        countText.SetActive(false);
#endif
    }


    protected void Add(UInt32 newCount)
    {
        if (newCount > 1)
        {
            InitText(newCount);
        }
    }

    protected bool Remove(UInt32 newCount)
    {
        if (newCount <= 1)
        {
            countText.SetActive(false);

            if (newCount == 0)
            {
                Destroy(itemTransform);
                itemTransform = null;
                return true;
            }
        }
        else
        {
            InitText(newCount);
        }
        return false;
    }


    public void Init(UInt32 id, UInt32 count)
    {
        itemTransform = Instantiate(InventoryManager.items[id].icon, transform).transform;
        itemTransform.SetAsFirstSibling();

        if (count > 1)
        {
            InitText(count);
        }
    }

    public void InitText(UInt32 count)
    {
        countText.SetActive(true);
        countText.GetComponent<TMP_Text>().text = count.ToString();
    }


    protected void ItemLost()
    {
        itemTransform = null;
    }
}
