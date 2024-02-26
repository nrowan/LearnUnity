using UnityEngine;
public class OxygenPod : ConsumableItem
{
    public OxygenPod(string displayName, string description, int quantity, Sprite image) :
        base(ConsumableItemNames.OxygenPod, displayName, description, quantity, image)
    {
    }
}