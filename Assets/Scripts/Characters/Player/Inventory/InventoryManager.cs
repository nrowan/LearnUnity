using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public ItemSlot[] ItemSlot;
    private GameObject _inventoryMenu;
    private GameObject _inventorySlots;
    private bool _inventoryOpen = false;

    private PlayerActions _playerActions;

    private void Awake()
    {
        _playerActions = new PlayerActions();
    }
    private void Start()
    {
        _playerActions.Player_Map.Inventory.performed += _ => InventoryAction();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "InventoryMenu")
            {
                _inventoryMenu = gameObject.transform.GetChild(i).gameObject;
                break;
            }
        }
    }
    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
        EventManager.OnItemAdded += AddItem;
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
        EventManager.OnItemAdded -= AddItem;
    }
    private void InventoryAction()
    {
        if (!_inventoryOpen)
        {
            Time.timeScale = 0;
            _inventoryOpen = true;
            _inventoryMenu.SetActive(true);
        }
        else
        {
            _inventoryMenu.SetActive(false);
            _inventoryOpen = false;
            Time.timeScale = 1;
        }
    }
    private void AddItem(Item item)
    {
        bool found = false;
        foreach (var itemSlot in ItemSlot)
        {
            if (itemSlot.Item != null && itemSlot.Item.Name == item.Name)
            {
                itemSlot.Quantity = item.Quantity;
                found = true;
                break;
            }
        }
        if (!found)
        {
            foreach (var itemSlot in ItemSlot)
            {
                if (itemSlot.Item == null)
                {
                    itemSlot.AddItem(item);
                    break;
                }
            }
        }
    }
}
