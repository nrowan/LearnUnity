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

    public GameObject _selectedShader;
    public bool _thisItemSelected;
    private int _index;

    private void OnEnable()
    {
        InventoryEventManager.OnInventoryDeselect += OnInventoryDeselect;
    }
    private void OnDisable()
    {
        InventoryEventManager.OnInventoryDeselect -= OnInventoryDeselect;
    }

    public void OnCreation(Sprite itemImage, int quantity, int index)
    {
        _index = index;
        UpdateSlot(itemImage, quantity);
    }

    public void UpdateSlot(Sprite itemImage, int quantity)
    {
        _quanityText.text = quantity.ToString();
        _itemImage.sprite = itemImage;
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
        InventoryEventManager.OnInventoryDeselect();
        _selectedShader.SetActive(true);
        _thisItemSelected = true;
        InventoryEventManager.RaiseOnInventorySelect(_index);
    }
    public void OnInventoryDeselect()
    {
        _selectedShader.SetActive(false);
        _thisItemSelected = false;
    }
    public void OnRightClick()
    {

    }
    public void OnDrag()
    {

    }
    public void OnDrop()
    {

    }
}