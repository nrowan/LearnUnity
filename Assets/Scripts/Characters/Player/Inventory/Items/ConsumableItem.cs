using UnityEngine;

public class ConsumableItem : ItemBase
{    ConsumableItemNames _itemName;
    public ConsumableItemNames ItemName { get { return _itemName; } }
    public ConsumableItem(ConsumableItemNames itemName, string displayName, string description, int quantity, Sprite image) :
        base(ItemTypes.Consumable, displayName, description, quantity, image)
    {
        _itemName = itemName;
    }
}