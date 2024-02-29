using UnityEngine;

public class InventoryPresenter : MonoBehaviour
{
    [SerializeField]
    InventorySO _playerInventoryData;
    InventoryUI _inventoryUI;
    private PlayerActions _playerActions;
    private ItemTypes _currentItemType = ItemTypes.Consumable;

    private void Awake()
    {
        _playerActions = new PlayerActions();
    }
    private void Start()
    {
        _inventoryUI = FindAnyObjectByType<InventoryUI>();
        _playerActions.Player_Map.Inventory.performed += _ => InventoryKeyPressed();
        ReplaceItemSlots();
    }
    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
        InventoryEventManager.OnItemAdded += AddItem;
        InventoryEventManager.OnInventorySelect += OnInventorySelect;
        InventoryEventManager.OnInventoryTypeChange += OnInventoryTypeChange;
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
        InventoryEventManager.OnItemAdded -= AddItem;
        InventoryEventManager.OnInventorySelect -= OnInventorySelect;
        InventoryEventManager.OnInventoryTypeChange -= OnInventoryTypeChange;
    }
    /// <summary>
    /// Show Hide inventory
    /// </summary>
    private void InventoryKeyPressed()
    {
        if (!_inventoryUI.InventoryOpen)
        {
            Time.timeScale = 0;
            _inventoryUI.Show();
        }
        else
        {
            Time.timeScale = 1;
            _inventoryUI.Hide();
        }
    }
    private void AddItem(ItemSO item, int quantity)
    {
        int index = _playerInventoryData.AddItem(item, quantity);
        if(item.ItemType == _currentItemType)
        {
            Debug.Log("Update Slot" + index);
            InventoryItem? itemSO = _playerInventoryData.GetInventoryItem(item.ItemType, index);
            if(itemSO != null)
            {
                _inventoryUI.UpdateItemSlot(((InventoryItem)itemSO).Item.ItemImage, ((InventoryItem)itemSO).Quantity, index);
            }
        }
    }

    /// <summary>
    /// Clear all slots and repopulate with items from inventory list
    /// </summary>
    public void ReplaceItemSlots()
    {
        _inventoryUI.ClearItemSlots();
        var inventory = _playerInventoryData.GetCurrentInventoryState(_currentItemType);
        for (int i = 0; i < inventory.Length; i++)
        {
            var item = inventory[i];
            _inventoryUI.UpdateItemSlot(item.Item.ItemImage, item.Quantity, i);
        }
    }

    /// <summary>
    /// When an slot is clicked, update the description on the right side
    /// </summary>
    /// <param name="slot"></param>
    private void OnInventorySelect(int? index)
    {
        if (index != null)
        {
            InventoryItem? item = _playerInventoryData.GetInventoryItem(_currentItemType, (int)index);
            if(item != null)
            {
                _inventoryUI.UpdateItemDescription(((InventoryItem)item).Item.DisplayName, ((InventoryItem)item).Item.Description, ((InventoryItem)item).Item.ItemImage);
            }
        }
        else
        {
            _inventoryUI.UpdateItemDescription("", "", Resources.Load<Sprite>("EmptySlot"));
        }
    }
    private void OnInventoryItemDrag(int index)
    {

    }
    private void OnInventoryItemSwap(int index1, int index2)
    {
        _playerInventoryData.SwapItems(_currentItemType, index1, index2);
        ReplaceItemSlots();
    }

    private void OnInventoryTypeChange(ItemTypes type)
    {
        _currentItemType = type;
        ReplaceItemSlots();
    }
}
