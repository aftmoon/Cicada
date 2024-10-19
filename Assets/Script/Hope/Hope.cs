using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Hope : MonoBehaviour
{
    //private Rigidbody2D rbA; // A�����Rigidbody
    //public Rigidbody2D rbB; // B�����Rigidbody
    //public float repulsionConstant = -100f; // �ų�������
    //public float thresholdDistance = 5f; // ������ֵ������С�����ֵʱ��ʼ�����ų���

    //private void Awake()
    //{
    //    rbA = GetComponent<Rigidbody2D>();

    //}
    //void FixedUpdate()
    //{
    //    // ����������ľ���
    //    Vector3 direction = rbB.position - rbA.position;
    //    float distance = direction.magnitude;

    //    // �������С����ֵ�������ų���
    //    if (distance < thresholdDistance && distance > 0f)
    //    {
    //        // �����ų�����С���ų��������ƽ���ɷ���
    //        float forceMagnitude = repulsionConstant / (distance * distance);

    //        // �������һ�����õ���������
    //        Vector3 forceDirection = direction.normalized;

    //        // ��B����ʩ�ӷ��������
    //        rbB.AddForce(forceDirection * forceMagnitude, ForceMode2D.Force);
    //    }
    //}
    private Rigidbody2D aRigidbody;  // a����ĸ���
    public Collider2D otherRigidbody; //��
    public float gravitationalForce = 100f;  // ��������ǿ��
    public float distanceThreshold = 5f;    // �����������ֵ

    
    public float speed;


    private void Awake()
    {
        aRigidbody = GetComponent<Rigidbody2D>();
        Cicada cicada = FindObjectOfType<Cicada>();
        if (cicada != null)
            speed = cicada.speed;
    }

    private void Update()
    {
        if (otherRigidbody != null)
            OnTriggerStay2D(otherRigidbody);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // ȷ��Ŀ��������b��������Rigidbody2D���
        Rigidbody2D bRigidbody = other.attachedRigidbody;
        if (bRigidbody != null && bRigidbody != aRigidbody)
        {
            // ����a��b֮��ķ���;���
            Vector2 direction = aRigidbody.position - bRigidbody.position;
            float distance = direction.magnitude;

            if (distance <= distanceThreshold)
            {

                if (distance >= 0.1f)
                {

                    // ��һ����������
                    direction.Normalize();

                    // ������������ʩ�Ӹ�b����
                    float num = Mathf.Max(Mathf.Pow(distance, 2), 2);
                    float forceMagnitude = gravitationalForce / num; // ������С�����ھ����ƽ��
                    Vector2 force = direction * forceMagnitude;

                    bRigidbody.AddForce(force);
                    //Debug.Log("1111");



                }
            }
        }
    }
}
