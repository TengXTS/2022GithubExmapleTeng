using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatInLight : MonoBehaviour
{
    public bool ifInLight = false;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ifInLight = true;
        // Debug.Log("1");
        

    }

    private void OnTriggerExit(Collider other)
    {
        ifInLight = false;
        // Debug.Log("0");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
