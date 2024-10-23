using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hope : MonoBehaviour
{
    //private Rigidbody2D rbA; // A刚体的Rigidbody
    //public Rigidbody2D rbB; // B刚体的Rigidbody
    //public float repulsionConstant = -100f; // 排斥力常数
    //public float thresholdDistance = 5f; // 距离阈值，距离小于这个值时开始产生排斥力

    //private void Awake()
    //{
    //    rbA = GetComponent<Rigidbody2D>();

    //}
    //void FixedUpdate()
    //{
    //    // 计算两刚体的距离
    //    Vector3 direction = rbB.position - rbA.position;
    //    float distance = direction.magnitude;

    //    // 如果距离小于阈值，计算排斥力
    //    if (distance < thresholdDistance && distance > 0f)
    //    {
    //        // 计算排斥力大小，排斥力与距离平方成反比
    //        float forceMagnitude = repulsionConstant / (distance * distance);

    //        // 将方向归一化，得到方向向量
    //        Vector3 forceDirection = direction.normalized;

    //        // 对B刚体施加反方向的力
    //        rbB.AddForce(forceDirection * forceMagnitude, ForceMode2D.Force);
    //    }
    //}
    private Rigidbody2D aRigidbody;  // a物体的刚体
    public Collider2D otherRigidbody; //蝉
    public float gravitationalForce = 100f;  // 吸引力的强度
    public float distanceThreshold = 5f;    // 吸引距离的阈值

    
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
        // 确保目标物体是b，并且有Rigidbody2D组件
        Rigidbody2D bRigidbody = other.attachedRigidbody;
        if (bRigidbody != null && bRigidbody != aRigidbody)
        {
            // 计算a和b之间的方向和距离
            Vector2 direction = aRigidbody.position - bRigidbody.position;
            float distance = direction.magnitude;

            if (distance <= distanceThreshold)
            {

                if (distance >= 0.1f)
                {

                    // 归一化方向向量
                    direction.Normalize();

                    // 计算引力，并施加给b刚体
                    float num = Mathf.Max(Mathf.Pow(distance, 2), 2);
                    float forceMagnitude = gravitationalForce / num; // 引力大小反比于距离的平方
                    Vector2 force = direction * forceMagnitude;

                    bRigidbody.AddForce(force);
                    //Debug.Log("1111");



                }
            }
        }
    }
}
