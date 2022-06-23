using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl2 : MonoBehaviour
{
    // Start is called before the first frame update
    private float cameraDistanceSpeed = 0;
    float cameraDistance = 0;

    void Start()
    {
        GetComponent<Transform>().position = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ExampleCoroutine());
        cameraDistance -= cameraDistanceSpeed;
        
        GetComponent<Transform>().position = new Vector3(0, 0, cameraDistance);
        if (cameraDistance <= -16)
        {
            StopAllCoroutines();
            cameraDistanceSpeed = 0;
            
        }
    }
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        cameraDistanceSpeed += 0.0003f;
    }
}
