using System.Xml.Serialization;
using UnityEngine;

public class RepelUI : MonoBehaviour
{
    private RectTransform uiElement;    // UI Ԫ�ص� RectTransform
    private Collider2D otherRigidbody; // Ŀ�����
    public float gravitationalForce = 100f;  // ��������ǿ��
    public float distanceThreshold = 100f;    // �������ľ�����ֵ

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
        // ��ȡ UI Ԫ�ص���������
        Vector3 uiWorldPos = uiElement.position;

        Rigidbody2D targetRigidbody = other.attachedRigidbody;
        Vector2 direction = (Vector2)(targetRigidbody.position - (Vector2)uiWorldPos);
        float distance = direction.magnitude;
        

        // ���� UI Ԫ�������֮��ľ���ͷ���


        if (targetRigidbody != null)
        {
            //Debug.Log(targetRigidbody.name);
            // �������С����ֵ��ʩ��������
            Debug.Log(distance);
            if (distance <= distanceThreshold && distance > 0.1f)
            {
                Debug.Log("1111");
                // ��һ����������
                direction.Normalize();

                // ������������С�������������ھ����ƽ��
                float forceMagnitude = gravitationalForce / Mathf.Max(distance * distance, 1f);
                Vector2 force = direction * forceMagnitude;

                // ʩ��������������
                targetRigidbody.AddForce(-force);  // �����������Ǹ��ģ�ʹ���忿�� UI Ԫ��
                
            }
        }
        else
        {
            Debug.LogWarning("targrtRigidbody is null");
        }
    }
}
