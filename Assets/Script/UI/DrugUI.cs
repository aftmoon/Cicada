using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>(); // �ҵ����� Canvas
    }

    // ����ʼ�϶�ʱ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ��������������κ���ϣ�����϶���ʼʱִ�е��߼�
    }

    // ���϶�ʱ����
    public void OnDrag(PointerEventData eventData)
    {
        // ʹ�� eventData.delta ������λ�õ�ƫ��
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // �������϶�ʱ����
    public void OnEndDrag(PointerEventData eventData)
    {
        // ��������������κ���ϣ�����϶�����ʱִ�е��߼�
    }
}

