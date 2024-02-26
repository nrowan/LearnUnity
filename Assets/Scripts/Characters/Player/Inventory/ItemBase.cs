using System;
using UnityEngine;

public abstract class ItemBase : IItem, IActionable
{
    ItemNames _itemName;
    public ItemNames ItemName { get { return _itemName; } }
    private ItemTypes _itemType;
    public ItemTypes ItemType { get { return _itemType; } }
    private string _displayName;
    public string DisplayName { get { return _displayName; } }
    private string _description;
    public string Description { get { return _description; } }
    private int _quantity;
    public int Quantity { get { return _quantity; } }
    private Guid _guid;
    public Guid Guid { get { return _guid; } }
    private Sprite _image;
    public Sprite Image { get { return _image; } }

    public ItemBase(ItemNames itemName, ItemTypes itemType, string displayName, string description, int quantity, Sprite image)
    {
        _itemName = itemName;
        _itemType = itemType;
        _displayName = displayName;
        _description = description;
        _quantity = quantity;
        _guid = new Guid();
        _image = image;
    }
}