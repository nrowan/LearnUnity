using UnityEngine;

public class ItemGO : MonoBehaviour {
    [SerializeField]
    private ItemSO _item;
    public int _quantity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InventoryEventManager.RaiseOnItemAdded(_item, _quantity);
            Destroy(gameObject);
        }
    }
}