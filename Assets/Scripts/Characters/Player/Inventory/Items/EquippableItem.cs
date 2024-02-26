using UnityEngine;

public class EquippableItem : ItemBase
{
    public EquippableItemNames _itemName = EquippableItemNames.AirTank;
    public override int ItemName { get { return (int)_itemName;}}
    public EquippableItem(EquippableItemNames itemName, string displayName, string description, int quantity, Sprite image) :
        base(ItemTypes.Equippable, displayName, description, quantity, image)
    {
        _itemName = itemName;
    }
}