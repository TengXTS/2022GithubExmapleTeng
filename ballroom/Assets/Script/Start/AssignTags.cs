using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignTags : MonoBehaviour
{
 
    void Awake()
    {
        GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].name.Contains("Thumb1"))
            {
                gos[i].tag = "thumb";
            }
            else if (gos[i].name.Contains("Index1"))
            {
                gos[i].tag = "index";
            }
            else if (gos[i].name.Contains("Middle1"))
            {
                gos[i].tag = "middle";
            }
            else if (gos[i].name.Contains("Ring1"))
            {
                gos[i].tag = "ring";
            }
            else if (gos[i].name.Contains("Pinky1"))
            {
                gos[i].tag = "pinky";
            }
            else if (gos[i].name.Contains("Hand_"))
            {
                gos[i].tag = "hand";
            }
            else if (gos[i].name.Contains("armRoll"))
            {
                gos[i].tag = "armRoll";
            }
            else if (gos[i].name.Contains("ForeArm_"))
            {
                gos[i].tag = "arm";
            }
        }

    }
}
