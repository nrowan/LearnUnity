using UnityEngine;


public class BaseItemGameObject : MonoBehaviour
{
    [SerializeField]
    protected string _displayName;
    [SerializeField]
    protected string _description;
    [SerializeField]
    protected int _quantity;
    [SerializeField]
    public Sprite _itemImage;
    protected ItemBase _item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EventManager.RaiseOnItemAdded(_item);
            Destroy(gameObject);
        }
    }
}