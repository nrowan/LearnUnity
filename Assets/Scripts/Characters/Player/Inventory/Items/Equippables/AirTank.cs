using UnityEngine;
public class AirTank : EquippableItem
{
    public AirTank(string displayName, string description, int quantity, Sprite image) :
        base(EquippableItemNames.AirTank, displayName, description, quantity, image)
    {
    }
}