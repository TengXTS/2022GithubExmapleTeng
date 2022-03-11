using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightHand : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject WebCamInPut;
    private Vector3 rightWrist;
    private Vector3 rightElbow;
    private Transform myTransform;
    private Vector3 rightShoulder;
    private Vector3 leftShoulder;
    private Vector3 rightHip;
    private Vector3 leftHip;
    public float lengthAdjust = 0.2f;
    void Start()
    {
        WebCamInPut = GameObject.FindGameObjectWithTag("webcam");
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        rightWrist = WebCamInPut.GetComponent<PoseVisuallizer3D>().rightWrist;
        rightElbow = WebCamInPut.GetComponent<PoseVisuallizer3D>().rightElbow;
        
        rightShoulder = WebCamInPut.GetComponent<PoseVisuallizer3D>().rightShoulder;
        leftShoulder = WebCamInPut.GetComponent<PoseVisuallizer3D>().leftShoulder;
        rightHip = WebCamInPut.GetComponent<PoseVisuallizer3D>().rightHip;
        leftHip = WebCamInPut.GetComponent<PoseVisuallizer3D>().leftHip;

        Vector2 centerPoint = (new Vector2(rightShoulder.x, rightShoulder.y) + new Vector2(leftShoulder.x, leftShoulder.y) + new Vector2(rightHip.x, rightHip.y) + new Vector2(leftHip.x, leftHip.y))/4;
        float centerLength = (new Vector2(rightWrist.x, rightWrist.y) - centerPoint).magnitude;
        
        myTransform.position = rightElbow;
        myTransform.LookAt(rightWrist);
        // Debug.Log(centerLength);
        // myTransform.Find("righthand").localScale = new Vector3(0.05f, 0.05f, centerLength);
        myTransform.localScale = new Vector3(1, 1, lengthAdjust * Mathf.Pow(2, (centerLength-0.3f) * 10));

    }
}
//需要一个身体模型，将landmark绑定骨骼点；布置环境，编程小球运动方式。
