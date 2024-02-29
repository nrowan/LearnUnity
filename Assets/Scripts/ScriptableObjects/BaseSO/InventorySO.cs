using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private ItemTypeHolder[] _inventoryItems = new ItemTypeHolder[] { };

    public int AddItem(ItemSO item, int quantity)
    {
        int foundIndex = -1;
        var itemsForType = GetItemsForType(item.ItemType);
        if (itemsForType == null)
        {
            var newInvHolder = new ItemTypeHolder
            {
                ItemType = item.ItemType,
                Items = new[]
                {
                    new InventoryItem
                    {
                        Item = item,
                        Quantity = quantity
                    }
                }
            };
            _inventoryItems = _inventoryItems.Append(newInvHolder).ToArray();
            foundIndex = 0;
        }
        else
        {
            bool found = false;
            for (int i = 0; i < itemsForType.Items.Length; i++)
            {
                if (itemsForType.Items[i].Item.Id == item.Id)
                {
                    itemsForType.Items[i] = itemsForType.Items[i].ChangeQuantity(quantity);
                    found = true;
                    foundIndex = i;
                    break;
                }
            }
            if (!found)
            {
                itemsForType.Items = itemsForType.Items.Append(new InventoryItem
                {
                    Item = item,
                    Quantity = quantity
                }).ToArray();
                foundIndex = itemsForType.Items.Length - 1;
            }
        }
        OutputAllItems();
        return foundIndex;
    }

    public ItemTypeHolder GetItemsForType(ItemTypes type)
    {
        foreach (var item in _inventoryItems)
        {
            if (item.ItemType == type)
            {
                return item;
            }
        }
        return null;
    }

    public void OutputAllItems()
    {
        foreach (var typeItem in _inventoryItems)
        {
            foreach (var item in typeItem.Items)
            {
                Debug.Log(typeItem.ItemType + ":" + item.Item.DisplayName + ":" + item.Quantity);
            }
        }
    }
    public InventoryItem[] GetCurrentInventoryState(ItemTypes type)
    {
        var itemsForType = GetItemsForType(type)?.Items;
        return itemsForType ?? new InventoryItem[] { };
    }
    public InventoryItem? GetInventoryItem(ItemTypes type, int index)
    {
        var itemsForType = GetItemsForType(type)?.Items;
        return itemsForType != null ? itemsForType[index] : null;
    }
    public void SwapItems(ItemTypes type, int index1, int index2)
    {
        /*InventoryItem item1 = _inventoryItems[type][index1];
        _inventoryItems[type][index1] = _inventoryItems[type][index2];
        _inventoryItems[type][index2] = item1;*/
    }
}

[Serializable]
public struct InventoryItem
{
    public int Quantity;
    public ItemSO Item;
    public InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem
        {
            Item = this.Item,
            Quantity = this.Quantity + newQuantity,
        };
    }
}

[Serializable]
public class ItemTypeHolder
{
    public ItemTypes ItemType;
    public InventoryItem[] Items;
}