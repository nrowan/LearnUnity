using UnityEngine;

public class AirTankGameObject : BaseItemGameObject
{
    [SerializeField]
    protected EquippableItemNames _itemName;
    private void Start()
    {
        _item = new EquippableItem(_itemName, _displayName, _description, _quantity, _itemImage);
    }
}