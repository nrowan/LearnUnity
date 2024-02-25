using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Item
{
    private string _name;
    public string Name { get { return _name; } }
    private string _displayName;
    public string DisplayName { get { return _displayName; } }
    private string _description;
    public string Description { get { return _description; } }
    private Guid _guid;
    public Guid Guid { get { return _guid; } }
    private Sprite _image;
    public Sprite Image { get { return _image; }}
    private int _quantity;
    public int Quantity { get { return _quantity; } }

    public Item(string name, string description, int quantity, Sprite image)
    {
        _name = name;
        _description = description;
        _quantity = quantity;
        _guid = new Guid();
        _image = image;
    }
}