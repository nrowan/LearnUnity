using UnityEngine;

public class OxygenPodGameObject : BaseItemGameObject
{
    [SerializeField] 
    private ConsumableItemNames _itemName;
    private void Start()
    {
        _item = new OxygenPod(_itemName, _displayName, _description, _quantity, _itemImage);
    }
}