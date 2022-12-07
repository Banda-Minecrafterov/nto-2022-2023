using UnityEngine.EventSystems;

public class HandSlot : InventorySlot
{
    GameSlot gameSlot;


    void Start()
    {
        gameSlot = InventoryManager.GetGameSlot(transform.GetSiblingIndex()).GetComponent<GameSlot>();
        gameSlot.slot = this;
    }


    protected override void AddObject()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.HandSlot0 + transform.GetSiblingIndex(), this);
    }


    public override void Init(uint id, uint count)
    {
        base.Init(id, count);
        gameSlot.Init(id, count);
    }


    public override bool Add(uint value)
    {
        if (base.Add(value))
        {
            gameSlot.Add(count);
            return true;
        }
        return false;
    }

    public override bool Remove(uint value)
    {
        if (base.Remove(value))
        {
            gameSlot.Remove(count);
            return true;
        }
        return false;
    }


    protected override void ItemDropped(PointerEventData eventData)
    {
        base.ItemDropped(eventData);

        gameSlot.Init(id, count);
    }


    protected override void ItemLost()
    {
        base.ItemLost();

        gameSlot.ItemLost();
    }
}
