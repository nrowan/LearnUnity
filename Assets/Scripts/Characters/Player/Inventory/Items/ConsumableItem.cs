using UnityEngine;

public class ConsumableItem : ItemBase
{
    public ConsumableItemNames _itemName;
    public override int ItemName { get { return (int)_itemName;}}
    public ConsumableItem(ConsumableItemNames itemName, string displayName, string description, int quantity, Sprite image) :
        base(ItemTypes.Consumable, displayName, description, quantity, image)
    {
        _itemName = itemName;
    }
}