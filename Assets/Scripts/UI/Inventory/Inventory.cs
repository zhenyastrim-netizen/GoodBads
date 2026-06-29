using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int slotCount = 36;
    public List<InventorySlot> slots = new List<InventorySlot>();

    private List<InventoryUI> inventoryUIs = new List<InventoryUI>();

    private void Awake()
    {
        slots.Clear();

        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    public void RegisterUI(InventoryUI ui)
    {
        if (!inventoryUIs.Contains(ui))
        {
            inventoryUIs.Add(ui);
        }
    }

    public void UpdateUI()
    {
        foreach (InventoryUI ui in inventoryUIs)
        {
            ui.UpdateUI();
        }
    }

    public bool AddItem(ItemData item, int amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.CanAdd(item))
            {
                int freeSpace = item.maxStack - slot.amount;
                int addAmount = Mathf.Min(freeSpace, amount);

                slot.amount += addAmount;
                amount -= addAmount;

                if (amount <= 0)
                {
                    UpdateUI();
                    return true;
                }
            }
        }

        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                int addAmount = Mathf.Min(item.maxStack, amount);

                slot.item = item;
                slot.amount = addAmount;
                amount -= addAmount;

                if (amount <= 0)
                {
                    UpdateUI();
                    return true;
                }
            }
        }

        UpdateUI();
        return false;
    }

    public void MoveOrSwapSlot(int fromIndex, int toIndex)
    {
        if (fromIndex == toIndex)
            return;

        InventorySlot fromSlot = slots[fromIndex];
        InventorySlot toSlot = slots[toIndex];

        if (fromSlot.IsEmpty())
            return;

        if (toSlot.IsEmpty())
        {
            toSlot.item = fromSlot.item;
            toSlot.amount = fromSlot.amount;
            fromSlot.Clear();
            UpdateUI();
            return;
        }

        if (toSlot.item == fromSlot.item)
        {
            int freeSpace = toSlot.item.maxStack - toSlot.amount;
            int moveAmount = Mathf.Min(freeSpace, fromSlot.amount);

            toSlot.amount += moveAmount;
            fromSlot.amount -= moveAmount;

            if (fromSlot.amount <= 0)
                fromSlot.Clear();

            UpdateUI();
            return;
        }

        ItemData tempItem = toSlot.item;
        int tempAmount = toSlot.amount;

        toSlot.item = fromSlot.item;
        toSlot.amount = fromSlot.amount;

        fromSlot.item = tempItem;
        fromSlot.amount = tempAmount;

        UpdateUI();
    }
}