using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Transform slotParent;
    public GameObject slotPrefab;

    [Header("Slot Range")]
    public int startIndex = 0;
    public int slotsToShow = 36;

    [Header("Hotbar")]
    public bool showSelection = false;
    public HotbarSelector hotbarSelector;

    private InventorySlotUI[] uiSlots;

    private void Start()
    {
        inventory.RegisterUI(this);
        CreateSlots();
        UpdateUI();
    }

    private void CreateSlots()
    {
        uiSlots = new InventorySlotUI[slotsToShow];

        for (int i = 0; i < slotsToShow; i++)
        {
            GameObject slotObject = Instantiate(slotPrefab, slotParent, false);

            InventorySlotUI slotUI = slotObject.GetComponent<InventorySlotUI>();

            int realSlotIndex = startIndex + i;

            slotUI.Setup(inventory, this, realSlotIndex);

            uiSlots[i] = slotUI;
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            int realSlotIndex = startIndex + i;

            uiSlots[i].SetSlot(inventory.slots[realSlotIndex]);

            bool selected = false;

            if (showSelection && hotbarSelector != null)
            {
                selected = realSlotIndex == hotbarSelector.SelectedSlotIndex;
            }

            uiSlots[i].SetSelected(selected);
        }
    }
}