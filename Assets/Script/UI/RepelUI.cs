using System.Xml.Serialization;
using UnityEngine;

public class RepelUI : MonoBehaviour
{
    private RectTransform uiElement;    // UI 元素的 RectTransform
    private Collider2D otherRigidbody; // 目标刚体
    public float gravitationalForce = 100f;  // 吸引力的强度
    public float distanceThreshold = 100f;    // 吸引力的距离阈值

    private void Awake()
    {
        GameObject playObject = GameObject.FindWithTag("Player");
        if (playObject != null)
        {
            otherRigidbody = playObject.GetComponent<Collider2D>();
        }
        else
        {
            Debug.LogWarning("playObject is null");
        }
        uiElement = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (otherRigidbody != null)
        {
            attract(otherRigidbody);
        }
        
    }

    private void attract(Collider2D other)
    {
        // 获取 UI 元素的世界坐标
        Vector3 uiWorldPos = uiElement.position;

        Rigidbody2D targetRigidbody = other.attachedRigidbody;
        Vector2 direction = (Vector2)(targetRigidbody.position - (Vector2)uiWorldPos);
        float distance = direction.magnitude;
        

        // 计算 UI 元素与刚体之间的距离和方向


        if (targetRigidbody != null)
        {
            //Debug.Log(targetRigidbody.name);
            // 如果距离小于阈值，施加吸引力
            Debug.Log(distance);
            if (distance <= distanceThreshold && distance > 0.1f)
            {
                Debug.Log("1111");
                // 归一化方向向量
                direction.Normalize();

                // 计算吸引力大小，吸引力反比于距离的平方
                float forceMagnitude = gravitationalForce / Mathf.Max(distance * distance, 1f);
                Vector2 force = direction * forceMagnitude;

                // 施加吸引力到刚体
                targetRigidbody.AddForce(-force);  // 吸引力方向是负的，使刚体靠近 UI 元素
                
            }
        }
        else
        {
            Debug.LogWarning("targrtRigidbody is null");
        }
    }
}
