public class OxygenPodGameObject : BaseItemGameObject
{    
    private void Start()
    {
        _item = new OxygenPod(_displayName, _description, _quantity, _itemImage);
    }
}