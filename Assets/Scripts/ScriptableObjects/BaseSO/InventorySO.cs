using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private Dictionary<ItemTypes, Dictionary<int, InventoryItem>> _inventoryItems = new Dictionary<ItemTypes, Dictionary<int, InventoryItem>>();
    public int AddItem(ItemSO item, int quantity)
    {
        int foundIndex = -1;
        Dictionary<int, InventoryItem> itemsOfType;
        if (!_inventoryItems.TryGetValue(item.ItemType, out itemsOfType))
        {
            _inventoryItems.Add(item.ItemType,
                new Dictionary<int, InventoryItem>
                {
                    {
                        0,
                        new InventoryItem
                        {
                            Item = item,
                            Quantity = quantity
                        }
                    }
                });
            foundIndex = _inventoryItems[item.ItemType].Count - 1;
        }
        else
        {
            bool found = false;
            for (int i = 0; i < itemsOfType.Count; i++)
            {
                if (itemsOfType[i].Item.Id == item.Id)
                {
                    itemsOfType[i] = itemsOfType[i].ChangeQuantity(quantity);
                    found = true;
                    foundIndex = i;
                    break;
                }
            }
            if (!found)
            {
                itemsOfType.Add(itemsOfType.Count, new InventoryItem
                {
                    Item = item,
                    Quantity = quantity
                });
                foundIndex = itemsOfType.Count - 1;
            }
        }
        return foundIndex;
    }

    public void OutputAllItems()
    {
        foreach (KeyValuePair<ItemTypes, Dictionary<int, InventoryItem>> entry in _inventoryItems)
        {
            foreach (KeyValuePair<int, InventoryItem> subkey in _inventoryItems[entry.Key])
            {
                Debug.Log(entry.Key + ": " + subkey.Key + ":" + subkey.Value.Item.DisplayName + ":" + subkey.Value.Quantity);
            }
        }
    }

    public void ClearInventory()
    {
        _inventoryItems.Clear();
    }
    public Dictionary<int, InventoryItem> GetCurrentInventoryState(ItemTypes type)
    {
        Dictionary<int, InventoryItem> returnValue;
        _inventoryItems.TryGetValue(type, out returnValue);
        return returnValue ?? new Dictionary<int, InventoryItem>();
    }
    public InventoryItem GetInventoryItem(ItemTypes type, int index)
    {
        return _inventoryItems[type][index];
    }
    public void SwapItems(ItemTypes type, int index1, int index2)
    {
        InventoryItem item1 = _inventoryItems[type][index1];
        _inventoryItems[type][index1] = _inventoryItems[type][index2];
        _inventoryItems[type][index2] = item1;
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