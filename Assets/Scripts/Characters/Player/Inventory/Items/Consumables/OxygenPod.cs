using UnityEngine;
public class OxygenPod : ConsumableItem
{
    public OxygenPod(ConsumableItemNames itemName, string displayName, string description, int quantity, Sprite image) :
        base(itemName, displayName, description, quantity, image)
    {
    }
}