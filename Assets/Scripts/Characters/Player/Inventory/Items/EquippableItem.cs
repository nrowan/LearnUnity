using UnityEngine;

public class EquippableItem : ItemBase
{
    public EquippableItem(ItemNames itemName, string displayName, string description, int quantity, Sprite image) :
        base(itemName, ItemTypes.Equippable, displayName, description, quantity, image)
    {
    }
}