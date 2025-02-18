using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class VisualCardsHandler : SingletonBehaviour<VisualCardsHandler>
{
    public Camera MainCam;

    private void Awake()
    {
        MainCam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
