using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetact : MonoBehaviour
{
    public string colliderName;
    public bool ifCollision;
    void OnTriggerEnter(Collider other)
    {
        colliderName = this.name;
        ifCollision = true;
    }
    
    void OnTriggerExit(Collider other)
    {
        colliderName = null;
    }
}
