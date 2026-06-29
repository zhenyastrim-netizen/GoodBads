using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    public CanvasGroup inventoryWindow;
    public StatsPanelUI statsPanelUI;

    private bool isOpen;

    private void Start()
    {
        CloseInventory();
    }

    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (isOpen)
                CloseInventory();
            else
                OpenInventory();
        }
    }

    private void OpenInventory()
    {
        isOpen = true;

        inventoryWindow.alpha = 1;
        inventoryWindow.interactable = true;
        inventoryWindow.blocksRaycasts = true;

        if (statsPanelUI != null)
            statsPanelUI.UpdateStats();
    }

    private void CloseInventory()
    {
        isOpen = false;

        inventoryWindow.alpha = 0;
        inventoryWindow.interactable = false;
        inventoryWindow.blocksRaycasts = false;
    }
}