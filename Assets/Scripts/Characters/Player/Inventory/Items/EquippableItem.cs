using UnityEngine;

public class EquippableItem : ItemBase
{
    EquippableItemNames _itemName;
    public EquippableItemNames ItemName { get { return _itemName; } }
    public EquippableItem(EquippableItemNames itemName, string displayName, string description, int quantity, Sprite image) :
        base(ItemTypes.Equippable, displayName, description, quantity, image)
    {
        _itemName = itemName;
    }
}