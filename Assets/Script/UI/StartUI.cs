using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartUI : MonoBehaviour
{
    public Button startButton;  // 关联UI按钮
    public Cicada cicada;  // 关联需要移动的物体

    void Awake()
    {
        // 添加按钮点击事件
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    // 当点击按钮时触发的事件
    void OnStartButtonClicked()
    {
        // 调用物体开始移动的方法
        cicada.StartMoving();
    }
}
