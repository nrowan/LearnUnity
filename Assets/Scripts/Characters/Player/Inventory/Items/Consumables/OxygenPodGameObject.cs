using UnityEngine;

public class OxygenPodGameObject : BaseItemGameObject
{    
    [SerializeField]
    protected ItemNames _itemName;
    private void Start()
    {
        _item = new ConsumableItem(_itemName, _displayName, _description, _quantity, _itemImage);
    }
}