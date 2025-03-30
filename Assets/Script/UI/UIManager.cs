//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class UIManager : MonoBehaviour
//{
//    public GameObject uiPrefab; // 你的 UI 预制体
//    public int uiCount = 5; // 生成的 UI 数量
//    public float verticalSpacing = 100f; // UI 元素之间的垂直间距

//    private Canvas canvas;

//    void Start()
//    {
//        canvas = FindObjectOfType<Canvas>();
//        if (canvas == null)
//        {
//            Debug.LogError("No Canvas found in the scene.");
//            return;
//        }

//        // 生成 UI 元素
//        for (int i = 0; i < uiCount; i++)
//        {
//            // 创建 UI 元素并设置为 Canvas 的子对象
//            GameObject uiElement = Instantiate(uiPrefab, canvas.transform);
//            RectTransform rectTransform = uiElement.GetComponent<RectTransform>();

//            // 设置位置
//            rectTransform.anchoredPosition = new Vector2(0, -i * verticalSpacing);
//            //Debug.Log($"Generated UI Element at position: {rectTransform.anchoredPosition}"); // 打印生成位置

//            // 添加 UI 拖拽功能
//            uiElement.AddComponent<DragUI>();

//            // 添加 UI 排斥功能
//            uiElement.AddComponent<RepelUI>();
//        }
//    }
//}
