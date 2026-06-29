using UnityEngine;

public enum ItemType
{
    Material,
    Weapon,
    Consumable
}

[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int maxStack = 64;

    public ItemType itemType;

    [Header("Weapon")]
    public GameObject heldPrefab;
}