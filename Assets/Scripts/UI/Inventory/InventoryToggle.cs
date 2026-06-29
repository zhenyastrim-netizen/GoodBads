using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryToggle : MonoBehaviour
{
    public CanvasGroup inventoryPanel;

    private bool isOpen = false;

    private void Start()
    {
        CloseInventory();
    }

    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (isOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    private void OpenInventory()
    {
        isOpen = true;

        inventoryPanel.alpha = 1;
        inventoryPanel.interactable = true;
        inventoryPanel.blocksRaycasts = true;
    }

    private void CloseInventory()
    {
        isOpen = false;

        inventoryPanel.alpha = 0;
        inventoryPanel.interactable = false;
        inventoryPanel.blocksRaycasts = false;
    }
}