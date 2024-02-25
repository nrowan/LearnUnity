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

    private void UpdateSlots()
    {
        foreach(var slot in _inventorySlots.GetComponentsInChildren<ItemSlot>())
        {
            Destroy(slot.gameObject);
        }
        List<ItemBucket> toShow = _showingConsumables ? _itemConsumables : _itemEquippables;
        foreach(var bucket in toShow)
        {
            GameObject slotGO = Instantiate(ItemSlotPrefab, _inventorySlots.transform);
            ItemSlot slot = slotGO.GetComponent<ItemSlot>();
            slot.OnCreated(bucket);
        }
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
        bool found = false;
        List<ItemBucket> toShow = item.ItemType == ItemTypes.Consumable ? _itemConsumables : _itemEquippables;
        foreach(var bucket in toShow)
        {
            if(bucket.Item.ItemType == item.ItemType) 
            {
                bucket.UpdateQuantity(bucket.Quantity + item.Quantity);
                found = true;
                break;
            }
        }
        if(!found)
        {
            toShow.Add(new ItemBucket(item));
        }
        if(item.ItemType == ItemTypes.Consumable && _showingConsumables || item.ItemType == ItemTypes.Equippable && !_showingConsumables)
        {
            UpdateSlots();
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
        UpdateSlots();
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
        UpdateSlots();
    }
}
