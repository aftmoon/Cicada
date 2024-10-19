using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartUI : MonoBehaviour
{
    public Button startButton;  // ����UI��ť
    public Cicada cicada;  // ������Ҫ�ƶ�������

    void Awake()
    {
        // ��Ӱ�ť����¼�
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    // �������ťʱ�������¼�
    void OnStartButtonClicked()
    {
        // �������忪ʼ�ƶ��ķ���
        cicada.StartMoving();
    }
}
