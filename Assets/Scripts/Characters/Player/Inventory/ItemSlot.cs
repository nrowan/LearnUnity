using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private ItemBucket _itemBucket;
    [SerializeField]
    private TMP_Text quanityText;
    [SerializeField]
    private Image itemImage;
    public void OnCreated(ItemBucket item)
    {
        _itemBucket = item;
        quanityText.text = _itemBucket.Item.Quantity.ToString();
        itemImage.sprite = _itemBucket.Item.Image;
    }
}