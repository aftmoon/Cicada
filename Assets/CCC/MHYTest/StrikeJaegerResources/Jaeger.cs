using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaeger : MonoBehaviour
{
    private Animator m_Animator;
    public float moveSpeed = 2;
    private float run = 0;
    public Vector3 move;
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetFloat("Speed", 0);
    }

    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        move = new Vector3(x, 0, z);
        transform.LookAt(transform.position + new Vector3(x, 0, z));
        transform.position += new Vector3(x, 0, z) * moveSpeed * Time.deltaTime;
        m_Animator.SetFloat("left", x);
        m_Animator.SetFloat("front", z);
        if (move.magnitude!= 0 &&(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
        {
            moveSpeed = 7;
            m_Animator.SetBool("Run", true);
            m_Animator.SetBool("Idle", false);
        }
        else
        {
            moveSpeed = 2;
            m_Animator.SetBool("Run", false);
            m_Animator.SetBool("Idle", true);

        }
       
    }

}