using UnityEngine;

public class AirTankGameObject : BaseItemGameObject
{
    [SerializeField]
    private EquippableItemNames _itemName;
    private void Start()
    {
        _item = new AirTank(_itemName, _displayName, _description, _quantity, _itemImage);
    }
}