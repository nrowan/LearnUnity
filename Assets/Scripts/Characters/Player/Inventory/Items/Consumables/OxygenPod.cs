using UnityEngine;
public class OxygenPod : ConsumableItem
{
    public OxygenPod(ItemNames itemName, string displayName, string description, int quantity, Sprite image) :
        base(itemName, displayName, description, quantity, image)
    {
    }
}