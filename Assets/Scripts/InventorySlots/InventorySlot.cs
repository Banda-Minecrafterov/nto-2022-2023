using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;


[RequireComponent(typeof(Graphic))]
public class InventorySlot : ItemSlot,
    IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler,
    IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler,
    ISaveLoadData
{
    public const UInt32 NoId = UInt32.MaxValue;
    public UInt32 id { get; protected set; } = NoId;
    public UInt32 count { get; protected set; } = 0;


    new protected void Awake()
    {
        AddObject();

        base.Awake();
    }


    public new virtual bool Add(UInt32 value)
    {
        count += value;

        base.Add(count);

        return true;
    }

    public new virtual bool Remove(UInt32 value)
    {
        if (count < value)
            return false;

        count -= value;

        if (base.Remove(count))
        {
            id = NoId;
        }
        return true;
    }


    public new virtual void Init(UInt32 id, UInt32 count)
    {
        this.id    = id;
        this.count = count;

        base.Init(id, count);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemTransform == null)
        {
            eventData.pointerDrag = null;
            return;
        }

        itemTransform.SetParent(transform.parent.parent);
        itemTransform.SetAsLastSibling();

        countText.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (id == NoId)
        {
            countText.SetActive(false);
        }
        else
        {
            Snap();
            countText.SetActive(true);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (id == NoId)
        {
            ItemDropped(eventData);
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (id == NoId)
        {
            eventData.pointerEnter = null;
            return;
        }

        TipManager.TooltipEnable(InventoryManager.items[id]);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        TipManager.TooltipFollow(eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TipManager.TooltipDisable();
    }


    void Snap()
    {
        itemTransform.SetParent(transform);
        itemTransform.SetAsFirstSibling();

        itemTransform.localPosition = InventoryManager.items[id].icon.transform.localPosition;
    }


    protected virtual void AddObject()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.InvSlot0 + transform.GetSiblingIndex(), this);
    }


    protected virtual void ItemDropped(PointerEventData eventData)
    {
        var invSlot = eventData.pointerDrag.GetComponent<InventorySlot>();

        itemTransform = invSlot.itemTransform;
        id    = invSlot.id;
        count = invSlot.count;

        invSlot.ItemLost();

        Snap();

        if (count > 1)
            InitText(count);
    }


    protected new virtual void ItemLost()
    {
        id = NoId;

        base.ItemLost();
    }


    public void Save(BinaryWriter data)
    {
        data.Write(id);
        if (id != NoId)
        {
            data.Write(count);
        }
    }

    public void Load(BinaryReader data, int version)
    {
        id = data.ReadUInt32();
        if (id != NoId)
        {
            count = data.ReadUInt32();

            Init(id, count);
        }
    }
}
