using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private ItemSlot _item;
    public void Awake()
    {
        _canvas = transform.root.GetComponent<Canvas>();
        _item = GetComponentInChildren<ItemSlot>();
    }
    public void SetData(Sprite itemImage, int quantity)
    {
        _item.UpdateSlot(itemImage, quantity);
    }
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)_canvas.transform,
            mousePosition,
            _canvas.worldCamera,
            out position
        );
        transform.position = _canvas.transform.TransformPoint(position);
    }
    public void Toggle(bool val)
    {
        gameObject.SetActive(val);
    }
}