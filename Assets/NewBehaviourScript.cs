using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool aButton = Input.GetButton("A Button");
        bool bButton = Input.GetButton("B Button");
        bool xButton = Input.GetButton("X Button");
        bool yButton = Input.GetButton("Y Button");
        float dpadHorizontal = Input.GetAxis("Dpad Horizontal");
        float dpadVertical = Input.GetAxis("Dpad Vertical");
        if (aButton)
        {
            Debug.Log("A");
        }
        if (bButton)
        {
            Debug.Log("B");
        }
         if (xButton)
        {
            Debug.Log("X");
        }
         if (yButton)
        {
            Debug.Log("Y");
        }
        if (dpadHorizontal < 0)
        {
            Debug.Log("dLeft");
        }
        if (dpadVertical < 0)
        {
            Debug.Log("dDown");
        }
         if (dpadHorizontal > 0)
        {
            Debug.Log("dRight");
        }
         if (dpadVertical > 0)
        {
            Debug.Log("dUp");
        }
    }
}
