using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    public MouseFollower _mouseFollower;
    private bool _inventoryOpen = false;
    public bool InventoryOpen { get { return _inventoryOpen; } }
    private GameObject _inventoryMenu;
    private GameObject _inventorySlots;
    private ItemDescription _inventoryDescription;
    public GameObject ItemSlotPrefab;
    public Button _consumableButton;
    public Button _equippableButton;
    private Image _consumableButtonImage;
    private Image _equippableButtonImage;
    private float _originalAlpha;
    private int _slotLength = 0;

    private void Awake()
    {
        _consumableButtonImage = _consumableButton.GetComponent<Image>();
        _equippableButtonImage = _equippableButton.GetComponent<Image>();
        _originalAlpha = _equippableButtonImage.color.a;
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
        _mouseFollower.Toggle(false);
    }
    private void Start()
    {
        _consumableButton.onClick.AddListener(ShowConsumable);
        _equippableButton.onClick.AddListener(ShowEquippables);
    }
    private void OnEnable()
    {
        InventoryEventManager.OnItemEndDrag += OnItemEndDrag;
    }

    private void OnDisable()
    {
        InventoryEventManager.OnItemEndDrag -= OnItemEndDrag;
    }
    public void Show()
    {
        _inventoryOpen = true;
        _inventoryMenu.SetActive(true);
    }
    public void Hide()
    {
        _inventoryMenu.SetActive(false);
        _inventoryOpen = false;
    }
    public void UpdateItemSlot(Sprite itemImage, int quantity, int index)
    {
        var slots = _inventorySlots.GetComponentsInChildren<ItemSlot>();
        if (index >= _slotLength)
        {
            GameObject slotGO = Instantiate(ItemSlotPrefab, _inventorySlots.transform);
            ItemSlot slot = slotGO.GetComponent<ItemSlot>();
            slot.OnCreation(itemImage, quantity, index);
            _slotLength++;
        }
        else
        {
            slots[index].UpdateSlot(itemImage, quantity);
        }
    }
    public void ClearItemSlots()
    {
        var slots = _inventorySlots.GetComponentsInChildren<ItemSlot>();
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        _slotLength = 0;
    }
    public void UpdateItemDescription(string displayName, string description, Sprite itemImage)
    {
        _inventoryDescription.ItemDescriptionNameText.text = displayName;
        _inventoryDescription.ItemDescriptionText.text = description;
        _inventoryDescription.ItemDescriptionImage.sprite = itemImage;
    }
    void ShowConsumable()
    {
        var tempColor = _consumableButtonImage.color;
        tempColor.a = 255;
        _consumableButtonImage.color = tempColor;

        tempColor = _equippableButtonImage.color;
        tempColor.a = _originalAlpha;
        _equippableButtonImage.color = tempColor;
        InventoryEventManager.RaiseOnInventoryTypeChange(ItemTypes.Consumable);
    }
    void ShowEquippables()
    {
        var tempColor = _equippableButtonImage.color;
        tempColor.a = 255;
        _equippableButtonImage.color = tempColor;

        tempColor = _consumableButtonImage.color;
        tempColor.a = _originalAlpha;
        _consumableButtonImage.color = tempColor;
        InventoryEventManager.RaiseOnInventoryTypeChange(ItemTypes.Equippable);
    }

    public void CreateDraggedItem(Sprite itemImage, int quantity)
    {
        _mouseFollower.Toggle(true);
        _mouseFollower.SetData(itemImage, quantity);
    }
    public void OnItemEndDrag()
    {
        _mouseFollower.Toggle(false);
    }
}
