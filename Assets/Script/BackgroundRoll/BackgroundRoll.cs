using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRoll : MonoBehaviour
{
    private Transform background;
    public Transform back;
    private Rigidbody2D rb;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        background = GetComponent<Transform>();
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if(background.transform.position.y != back.transform.position.y)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed * Time.deltaTime);
        }
        if (background.transform.position.y >= back.transform.position.y)
        {
            background.transform.position = Vector2.zero;
        }
        
    }
}
