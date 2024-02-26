using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    private List<ItemBucket> _itemConsumables = new List<ItemBucket>();
    private List<ItemBucket> _itemEquippables = new List<ItemBucket>();
    private bool _inventoryOpen = false;
    private bool _showingConsumables = true;
    private PlayerActions _playerActions;

    private GameObject _inventoryMenu;
    private GameObject _inventorySlots;
    private ItemDescription _inventoryDescription;
    public GameObject ItemSlotPrefab;
    public Button _consumableButton;
    public Button _equippableButton;
    private Image _consumableButtonImage;
    private Image _equippableButtonImage;
    private float _originalAlpha;

    private void Awake()
    {
        _playerActions = new PlayerActions();
        _consumableButtonImage = _consumableButton.GetComponent<Image>();
        _equippableButtonImage = _equippableButton.GetComponent<Image>();
        _originalAlpha = _equippableButtonImage.color.a;
    }
    private void Start()
    {
        _consumableButton.onClick.AddListener(ShowConsumable);
        _equippableButton.onClick.AddListener(ShowEquippable);

        _playerActions.Player_Map.Inventory.performed += _ => InventoryAction();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "InventoryMenu")
            {
                _inventoryMenu = gameObject.transform.GetChild(i).gameObject;
                _inventorySlots = _inventoryMenu.GetComponentsInChildren<Transform>(true).FirstOrDefault(go => go.name == "InventorySlots").gameObject;
                _inventoryDescription = _inventoryMenu.GetComponentInChildren<ItemDescription>();
                break;
            }
        }
    }
    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
        EventManager.OnItemAdded += AddItem;
        EventManager.OnInventorySelect += InventorySelect;
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
        EventManager.OnItemAdded -= AddItem;
        EventManager.OnInventorySelect -= InventorySelect;
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
    private void AddItem(ItemBase item)
    {
        int updatedIndex = -1;
        ItemBucket newBucket = null;
        List<ItemBucket> toShow = item.ItemType == ItemTypes.Consumable ? _itemConsumables : _itemEquippables;
        for (int i = 0; i < toShow.Count; i++)
        {
            if (toShow[i].Item.ItemName == item.ItemName)
            {
                newBucket = toShow[i];
                newBucket.Quantity += item.Quantity;
                updatedIndex = i;
                break;
            }
        }
        // Not found, so new item
        if (newBucket == null)
        {
            newBucket = new ItemBucket(item);
            toShow.Add(newBucket);
            updatedIndex = toShow.Count - 1;
        }
        // If currently shown inventory type, update the slot with new value
        if (item.ItemType == ItemTypes.Consumable && _showingConsumables || item.ItemType == ItemTypes.Equippable && !_showingConsumables)
        {
            UpdateItemSlots(updatedIndex, newBucket);
        }
    }

    private void CreateNewItemSlot(ItemBucket bucket)
    {
        GameObject slotGO = Instantiate(ItemSlotPrefab, _inventorySlots.transform);
        ItemSlot slot = slotGO.GetComponent<ItemSlot>();
        slot.UpdateSlot(bucket);
    }
    private void UpdateItemSlots(int index, ItemBucket bucket)
    {
        var slots = _inventorySlots.GetComponentsInChildren<ItemSlot>();
        if (index > slots.Length - 1)
        {
            CreateNewItemSlot(bucket);
        }
        else
        {
            slots[index].UpdateSlot(bucket);
        }
    }
    private void ReplaceItemSlots()
    {
        foreach (var slot in _inventorySlots.GetComponentsInChildren<ItemSlot>())
        {
            Destroy(slot.gameObject);
        }
        List<ItemBucket> toShow = _showingConsumables ? _itemConsumables : _itemEquippables;
        foreach (var bucket in toShow)
        {
            CreateNewItemSlot(bucket);
        }
    }
    private void InventorySelect(ItemSlot slot)
    {
        if (slot != null)
        {
            _inventoryDescription.ItemDescriptionNameText.text = slot.ItemBucket.Item.DisplayName;
            _inventoryDescription.ItemDescriptionText.text = slot.ItemBucket.Item.Description;
            _inventoryDescription.ItemDescriptionImage.sprite = slot.ItemBucket.Item.Image;
        }
        else
        {
            _inventoryDescription.ItemDescriptionNameText.text = "";
            _inventoryDescription.ItemDescriptionText.text = "";
            _inventoryDescription.ItemDescriptionImage.sprite = Resources.Load<Sprite>("EmptySlot");
        }
    }
    void ShowConsumable()
    {
        _showingConsumables = true;
        var tempColor = _consumableButtonImage.color;
        tempColor.a = 255;
        _consumableButtonImage.color = tempColor;

        tempColor = _equippableButtonImage.color;
        tempColor.a = _originalAlpha;
        _equippableButtonImage.color = tempColor;
        ReplaceItemSlots();
        EventManager.RaiseOnInventorySelect(null);
    }
    void ShowEquippable()
    {
        _showingConsumables = false;
        var tempColor = _equippableButtonImage.color;
        tempColor.a = 255;
        _equippableButtonImage.color = tempColor;

        tempColor = _consumableButtonImage.color;
        tempColor.a = _originalAlpha;
        _consumableButtonImage.color = tempColor;
        ReplaceItemSlots();
        EventManager.RaiseOnInventorySelect(null);
    }
}
