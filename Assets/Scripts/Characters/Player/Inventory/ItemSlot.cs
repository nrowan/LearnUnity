using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item Slot
    [SerializeField]
    private TMP_Text _quanityText;
    [SerializeField]
    private Image _itemImage;

    private ItemBucket _itemBucket;
    public ItemBucket ItemBucket { get { return _itemBucket; } }
    public GameObject _selectedShader;
    public bool _thisItemSelected;

    private void OnEnable()
    {
        EventManager.OnInventoryDeselect += OnInventoryDeselect;
    }
    private void OnDisable()
    {
        EventManager.OnInventoryDeselect -= OnInventoryDeselect;
    }

    public void UpdateSlot(ItemBucket item)
    {
        _itemBucket = item;
        _quanityText.text = _itemBucket.Quantity.ToString();
        _itemImage.sprite = _itemBucket.Item.Image;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SelectSlot();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }
    public void SelectSlot()
    {
        EventManager.OnInventoryDeselect();
        _selectedShader.SetActive(true);
        _thisItemSelected = true;
        EventManager.RaiseOnInventorySelect(this);
    }
    public void OnInventoryDeselect()
    {
        _selectedShader.SetActive(false);
        _thisItemSelected = false;
    }
    public void OnRightClick()
    {

    }
}