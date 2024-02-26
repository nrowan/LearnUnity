using UnityEngine;

public class AirTankGameObject : BaseItemGameObject
{
    [SerializeField]
    protected ItemNames _itemName;
    private void Start()
    {
        _item = new EquippableItem(_itemName, _displayName, _description, _quantity, _itemImage);
    }
}