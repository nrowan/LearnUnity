using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableItem", menuName = "Inventory/ConsumableItem", order = 0)]
public class ConsumableItemSO : ItemSO, IDestropyableItem, IItemAction
{
    ConsumableItemSO() : base(ItemTypes.Consumable) {  }

    public string ActionName => "Consume";

    public bool PerformAction(GameObject character)
    {
        throw new System.NotImplementedException();
    }
}