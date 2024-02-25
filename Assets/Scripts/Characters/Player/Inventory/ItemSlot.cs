using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private Item _item;
    public Item Item { get { return _item; } }
    private int _quantity;
    public int Quantity { get { return _quantity; } set { _quantity += value; } }

    [SerializeField]
    private TMP_Text quanityText;
    [SerializeField]
    private Image itemImage;

    public void AddItem(Item item)
    {
        _item = item;
        _quantity = item.Quantity;
        quanityText.text = _quantity.ToString();
        itemImage.sprite = item.Image;
    }
}