using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image icon;
    public TMP_Text amountText;
    public GameObject selectedFrame;

    private Inventory inventory;
    private InventoryUI inventoryUI;
    private int slotIndex;

    private static InventorySlotUI draggedSlot;

    public void Setup(Inventory inventory, InventoryUI inventoryUI, int slotIndex)
    {
        this.inventory = inventory;
        this.inventoryUI = inventoryUI;
        this.slotIndex = slotIndex;
    }

    public void SetSlot(InventorySlot slot)
    {
        icon.rectTransform.anchoredPosition = Vector2.zero;
        icon.raycastTarget = false;
        amountText.raycastTarget = false;

        if (slot == null || slot.IsEmpty())
        {
            icon.enabled = false;
            icon.sprite = null;
            amountText.text = "";
            return;
        }

        icon.enabled = true;
        icon.sprite = slot.item.icon;

        if (slot.amount > 1)
            amountText.text = slot.amount.ToString();
        else
            amountText.text = "";
    }

    public void SetSelected(bool value)
    {
        if (selectedFrame != null)
        {
            selectedFrame.SetActive(value);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inventory.slots[slotIndex].IsEmpty())
            return;

        draggedSlot = this;
        icon.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedSlot != this)
            return;

        icon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggedSlot = null;
        icon.rectTransform.anchoredPosition = Vector2.zero;

        if (inventoryUI != null)
            inventoryUI.UpdateUI();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (draggedSlot == null)
            return;

        inventory.MoveOrSwapSlot(draggedSlot.slotIndex, slotIndex);
    }
}