using UnityEngine.Events;

public static class InventoryEventManager
{
    public static UnityAction<ItemSO, int> OnItemAdded;
    public static void RaiseOnItemAdded(ItemSO item, int quantity) => OnItemAdded?.Invoke(item, quantity);
    public static UnityAction OnInventoryDeselect;
    public static void RaiseOnInventoryDeselect() => OnInventoryDeselect?.Invoke();
    public static UnityAction<int?> OnInventorySelect;
    public static void RaiseOnInventorySelect(int? index) => OnInventorySelect?.Invoke(index);
    public static UnityAction<ItemTypes> OnInventoryTypeChange;
    public static void RaiseOnInventoryTypeChange(ItemTypes type) => OnInventoryTypeChange?.Invoke(type);
}