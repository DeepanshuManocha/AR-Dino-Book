﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up, Time.deltaTime*30);
        if(Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up, -Time.deltaTime*30);
    }
}
