public class ItemBucket
{
    private ItemBase _item;
    public ItemBase Item { get { return _item; } }
    private int _quantity;
    public int Quantity { get { return _quantity; } set { _quantity = value; } }

    public ItemBucket(ItemBase item)
    {
        _item = item;
        _quantity = item.Quantity;
    }
}