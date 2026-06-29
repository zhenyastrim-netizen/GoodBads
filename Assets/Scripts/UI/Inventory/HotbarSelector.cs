using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarSelector : MonoBehaviour
{
    public Inventory inventory;
    public Transform handPoint;

    public int SelectedSlotIndex { get; private set; } = 0;

    private GameObject currentHeldObject;
    private IUsableItem currentUsableItem;

    private void Start()
    {
        SelectSlot(0);
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;

        if (keyboard.digit1Key.wasPressedThisFrame) SelectSlot(0);
        if (keyboard.digit2Key.wasPressedThisFrame) SelectSlot(1);
        if (keyboard.digit3Key.wasPressedThisFrame) SelectSlot(2);
        if (keyboard.digit4Key.wasPressedThisFrame) SelectSlot(3);
        if (keyboard.digit5Key.wasPressedThisFrame) SelectSlot(4);
        if (keyboard.digit6Key.wasPressedThisFrame) SelectSlot(5);
        if (keyboard.digit7Key.wasPressedThisFrame) SelectSlot(6);
        if (keyboard.digit8Key.wasPressedThisFrame) SelectSlot(7);
        if (keyboard.digit9Key.wasPressedThisFrame) SelectSlot(8);

        float scroll = mouse.scroll.ReadValue().y;

        if (scroll > 0) SelectSlot(SelectedSlotIndex - 1);
        if (scroll < 0) SelectSlot(SelectedSlotIndex + 1);

        if (mouse.leftButton.isPressed)
        {
            UseSelectedItem();
        }

        if (keyboard.rKey.wasPressedThisFrame)
        {
            ReloadSelectedItem();
        }
    }

    private void SelectSlot(int index)
    {
        if (index < 0)
            index = 8;

        if (index > 8)
            index = 0;

        SelectedSlotIndex = index;

        EquipSelectedItem();

        if (inventory != null)
            inventory.UpdateUI();
    }

    private void EquipSelectedItem()
    {
        if (currentHeldObject != null)
        {
            Destroy(currentHeldObject);
        }

        currentHeldObject = null;
        currentUsableItem = null;

        InventorySlot slot = inventory.slots[SelectedSlotIndex];

        if (slot.IsEmpty())
            return;

        ItemData item = slot.item;

        if (item.itemType != ItemType.Weapon)
            return;

        if (item.heldPrefab == null)
            return;

        currentHeldObject = Instantiate(item.heldPrefab, handPoint);
        currentHeldObject.transform.localPosition = Vector3.zero;
        currentHeldObject.transform.localRotation = Quaternion.identity;

        currentUsableItem = currentHeldObject.GetComponent<IUsableItem>();
    }

    private void UseSelectedItem()
    {
        if (currentUsableItem != null)
        {
            currentUsableItem.Use();
        }
    }

    private void ReloadSelectedItem()
    {
        if (currentUsableItem != null)
        {
            currentUsableItem.Reload();
        }
    }

    public ItemData GetSelectedItem()
    {
        InventorySlot slot = inventory.slots[SelectedSlotIndex];

        if (slot.IsEmpty())
            return null;

        return slot.item;
    }
}