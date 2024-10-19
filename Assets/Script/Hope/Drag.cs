using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    Vector2 mousePos;
    Vector2 distance;
    Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Camera.main.transform.position.z * -1; // …Ë÷√z÷µ
        mousePos = Camera.main.ScreenToWorldPoint(screenPos);
        //Debug.Log("Mouse Position: " + mousePos);
    }

    private void OnMouseDown()
    {
        distance = new Vector2(transform.position.x, transform.position.y) - mousePos;
    }

    private void OnMouseDrag()
    {
        transform.position = mousePos + distance;
        //Debug.Log("Mouse Position: " + mousePos + ", Object Position: " + transform.position);
        //rb2D.MovePosition(mousePos + distance);
    }
}
