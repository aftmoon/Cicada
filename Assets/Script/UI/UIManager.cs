using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject uiPrefab; // ��� UI Ԥ����
    public int uiCount = 5; // ���ɵ� UI ����
    public float verticalSpacing = 100f; // UI Ԫ��֮��Ĵ�ֱ���

    private Canvas canvas;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("No Canvas found in the scene.");
            return;
        }

        // ���� UI Ԫ��
        for (int i = 0; i < uiCount; i++)
        {
            // ���� UI Ԫ�ز�����Ϊ Canvas ���Ӷ���
            GameObject uiElement = Instantiate(uiPrefab, canvas.transform);
            RectTransform rectTransform = uiElement.GetComponent<RectTransform>();

            // ����λ��
            rectTransform.anchoredPosition = new Vector2(0, -i * verticalSpacing);
            //Debug.Log($"Generated UI Element at position: {rectTransform.anchoredPosition}"); // ��ӡ����λ��

            // ��� UI ��ק����
            uiElement.AddComponent<DragUI>();

            // ��� UI �ų⹦��
            uiElement.AddComponent<RepelUI>();
        }
    }
}
