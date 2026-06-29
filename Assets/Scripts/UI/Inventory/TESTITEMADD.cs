using UnityEngine;
using UnityEngine.InputSystem;

public class TESTITEMADD : MonoBehaviour
{
    public Inventory inventory;
    public ItemData testItem;
    public int amount = 1;

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Нажата E");

            if (inventory == null)
            {
                Debug.LogError("Inventory не назначен!");
                return;
            }

            if (testItem == null)
            {
                Debug.LogError("Test Item не назначен!");
                return;
            }

            bool added = inventory.AddItem(testItem, amount);
            Debug.Log("Предмет добавлен: " + added);
        }
    }
}