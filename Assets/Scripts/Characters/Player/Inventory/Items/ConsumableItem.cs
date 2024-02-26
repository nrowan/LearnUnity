using UnityEngine;

public class ConsumableItem : ItemBase
{    
    public ConsumableItem(ItemNames itemName, string displayName, string description, int quantity, Sprite image) :
        base(itemName, ItemTypes.Consumable, displayName, description, quantity, image)
    {
    }
}