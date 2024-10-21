using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cicada : MonoBehaviour
{
    Rigidbody2D rb;
    private bool isMoving = false;

    [Header("基本参数")]
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.gravityScale = 0.26F;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            Move();
        }
        else
        {
            rb.gravityScale = 0;
        }
    }

    public void Move()
    {
        rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
    }

    public void StartMoving()
    {
        isMoving = true;
    }
}
