using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetact : MonoBehaviour
{
    public string colliderName;
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        colliderName = this.name;
    }
    
    void OnTriggerExit(Collider other)
    {
        colliderName = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
