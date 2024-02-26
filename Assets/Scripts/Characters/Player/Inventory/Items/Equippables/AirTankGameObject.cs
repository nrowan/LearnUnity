public class AirTankGameObject : BaseItemGameObject
{
    private void Start()
    {
        _item = new AirTank(_displayName, _description, _quantity, _itemImage);
    }
}