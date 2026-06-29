[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int amount;

    public bool IsEmpty()
    {
        return item == null || amount <= 0;
    }

    public bool CanAdd(ItemData newItem)
    {
        return item == newItem && amount < item.maxStack;
    }

    public void Clear()
    {
        item = null;
        amount = 0;
    }
}