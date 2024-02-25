using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class ItemGameObject : MonoBehaviour
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _displayName;
    [SerializeField]
    private string _description;
    [SerializeField]
    private int _quantity;
    [SerializeField]
    public Sprite _itemImage;
    private Item _item;

    private void Start()
    {
        _item = new Item(_name, _description, _quantity, _itemImage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EventManager.RaiseOnItemAdded(_item);
            Destroy(gameObject);
        }
    }
}