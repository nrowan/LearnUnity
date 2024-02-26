using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private ItemBucket _itemBucket;
    [SerializeField]
    private TMP_Text quanityText;
    [SerializeField]
    private Image itemImage;
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

    public void OnCreated(ItemBucket item)
    {
        _itemBucket = item;
        quanityText.text = _itemBucket.Quantity.ToString();
        itemImage.sprite = _itemBucket.Item.Image;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            SelectSlot();
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }
    public void SelectSlot()
    {
        EventManager.OnInventoryDeselect();
        _selectedShader.SetActive(true);
        _thisItemSelected = true;
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