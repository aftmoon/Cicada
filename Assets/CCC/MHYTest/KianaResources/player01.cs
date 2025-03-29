using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class player01 : MonoBehaviour
{
    private Animator m_Animator;
    public float moveSpeed = 2;
    private float run=0;
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
        //run = m_Animator.GetFloat("Speed");
        m_Animator.SetFloat("Speed", move.magnitude);
        if (move.magnitude != 0 && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))) 
        {
            moveSpeed = 6;
            m_Animator.SetBool("IsRunning", true);
            m_Animator.SetBool("Idle", false);
        }
        else
        {
            moveSpeed = 2;
            m_Animator.SetBool("IsRunning", false);
            m_Animator.SetBool("Idle", true);

        }
        if (Input.GetKey(KeyCode.J))
        {
            m_Animator.SetBool("Jump", true);
            moveSpeed *= 1.2F;
        }


    }
    public void StopJump()
    {
        m_Animator.SetBool("Jump", false);
        //if (m_Animator.GetBool("IsRunning"))
        //{
        //    moveSpeed = 6;
        //}
        //if (m_Animator.GetBool("Idle"))
        //{
        //    moveSpeed = 2;
        //}
    }

}
