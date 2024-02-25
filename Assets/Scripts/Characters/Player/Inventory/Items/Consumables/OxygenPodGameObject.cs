using UnityEngine;

public class OxygenPodGameObject : BaseItemGameObject
{    
    [SerializeField]
    protected ConsumableItemNames _itemName;
    private void Start()
    {
        _item = new ConsumableItem(_itemName, _displayName, _description, _quantity, _itemImage);
    }
}