using System;
using UnityEngine;

public interface IItem
{
     public ItemTypes ItemType  { get; }
     public string DisplayName  { get; }
     public string Description  { get; }
     public int Quantity { get; }
     public Guid Guid  { get; }
     public Sprite Image { get; }
}