using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBacon : MonoBehaviour
{
    public bool breakEnd = false;
    public int count = 0;
    Rigidbody2D rb;
    GameObject GameObject;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject = rb.gameObject;
    }


}
