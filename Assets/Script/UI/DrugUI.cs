using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>(); // 找到父级 Canvas
    }

    // 当开始拖动时触发
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 可以在这里添加任何你希望在拖动开始时执行的逻辑
    }

    // 当拖动时触发
    public void OnDrag(PointerEventData eventData)
    {
        // 使用 eventData.delta 来控制位置的偏移
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // 当结束拖动时触发
    public void OnEndDrag(PointerEventData eventData)
    {
        // 可以在这里添加任何你希望在拖动结束时执行的逻辑
    }
}

