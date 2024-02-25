using UnityEngine;
public class AirTank : EquippableItem
{
    public AirTank(EquippableItemNames itemName, string displayName, string description, int quantity, Sprite image) :
        base(itemName, displayName, description, quantity, image)
    {
    }
}