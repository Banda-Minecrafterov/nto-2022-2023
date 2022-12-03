using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;

[RequireComponent(typeof(CanvasRenderer))]
public class InventorySlot : Graphic, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, ISaveLoadData
{
    public Transform itemTransform { get; private set; }

    GameObject countText;

    public const UInt32 NoId = UInt32.MaxValue;
    public UInt32 id    { get; private set; } = NoId;
    public UInt32 count { get; private set; } = 0;


    protected override void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.invSlot0 + transform.GetSiblingIndex(), this);

        countText = transform.Find("Count").gameObject;

#if DEBUG
        countText.SetActive(false);

        if (transform.childCount > 1)
        {
            itemTransform = transform.GetChild(0).transform;
            id    = 1;
            count = 1;
        }
#endif
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemTransform == null)
        {
            eventData.pointerDrag = null;
            return;
        }

        itemTransform.SetParent(InventoryMenu.menu.transform);
        itemTransform.SetAsLastSibling();

        countText.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemTransform.position = Input.mousePosition;
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
            var invSlot = eventData.pointerDrag.GetComponent<InventorySlot>();

            itemTransform = invSlot.itemTransform;
            id    = invSlot.id;
            count = invSlot.count;

            invSlot.itemTransform = null;
            invSlot.id = NoId;

            Snap();
            InitText();
        }
    }


    public void Init(UInt32 id, UInt32 count)
    {
        this.id    = id;
        this.count = count;

        Init();
    }

    public bool Add(UInt32 value)
    {
        count += value;

        if (count >= 1)
        {
            InitText();
        }

        return true;
    }

    public bool Remove(UInt32 value)
    {
        if (count < value)
            return false;

        count -= value;

        if (count <= 1)
        {
            countText.SetActive(false);

            if (count == 0)
            {
                id = NoId;
            }
        }
        else
        {
            InitText();
        }
        return true;
    }


    void Init()
    {
        itemTransform = Instantiate(InventoryMenu.datas[id].icon, transform).transform;
        itemTransform.SetAsFirstSibling();

        InitText();
    }

    void InitText()
    {
        countText.SetActive(true);
        countText.GetComponent<TMP_Text>().text = count.ToString();
    }

    void Snap()
    {
        itemTransform.SetParent(transform);
        itemTransform.SetAsFirstSibling();

        itemTransform.localPosition = InventoryMenu.datas[id].icon.transform.localPosition;
    }


    public void Save(ref BinaryWriter data)
    {
        data.Write(id);
        if (id != NoId)
        {
            data.Write(count);
        }
    }

    public void Load(ref BinaryReader data, int version)
    {
        id = data.ReadUInt32();
        if (id != NoId)
        {
            count = data.ReadUInt32();

            Init();
        }
    }
}
