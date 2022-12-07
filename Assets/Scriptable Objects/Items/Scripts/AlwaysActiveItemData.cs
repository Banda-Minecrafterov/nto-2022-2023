public class AlwaysActiveItemData : InventoryItemData
{
    public override bool StartChecking(GameSlot item)
    {
        return false;
    }
}
