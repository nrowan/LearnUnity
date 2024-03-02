using UnityEngine;

[CreateAssetMenu(fileName = "EquippableItem", menuName = "Inventory/EquippableItem", order = 0)]
public class EquippableItemSO : ItemSO
{
    EquippableItemSO() : base(ItemTypes.Equippable) {  }
}