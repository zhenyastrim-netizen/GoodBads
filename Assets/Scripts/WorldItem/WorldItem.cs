using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public ItemData item;
    public int amount = 1;

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (item != null && spriteRenderer != null)
            spriteRenderer.sprite = item.icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inventory inventory = collision.GetComponent<Inventory>();

        if (inventory == null)
            inventory = collision.GetComponentInParent<Inventory>();

        if (inventory == null)
            return;

        bool added = inventory.AddItem(item, amount);

        if (added)
        {
            Destroy(gameObject);
        }
    }
}