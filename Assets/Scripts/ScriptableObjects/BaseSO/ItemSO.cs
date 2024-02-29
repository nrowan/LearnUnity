using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 0)]
public class ItemSO : ScriptableObject
{
    [field: SerializeField]
    public ItemTypes ItemType { get; set; }
    [field: SerializeField]
    public bool IsStackable { get; set; } = true;
    public int Id => GetInstanceID();
    [field: SerializeField]
    public int MaxStackSize { get; set; } = 1;
    [field: SerializeField]
    public string DisplayName { get; set; }
    [field: SerializeField]
    [field: TextArea]
    public string Description { get; set; }
    [field: SerializeField]
    public Sprite ItemImage { get; set; }
}