using UnityEngine;
public class AirTank : EquippableItem
{
    public AirTank(ItemNames itemName, string displayName, string description, int quantity, Sprite image) :
        base(itemName, displayName, description, quantity, image)
    {
    }
}