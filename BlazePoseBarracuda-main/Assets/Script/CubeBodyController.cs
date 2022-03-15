using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBodyController : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject WebCamInPut;
    private GameObject rightHand;
    private GameObject leftHand;
    private GameObject head;
    private Vector3 rightWrist;
    private Vector3 leftWrist;
    private Vector3 rightElbow;
    private Vector3 leftElbow;
    private Transform rightHandTransform;
    private Transform leftHandTransform;
    private Transform headTransform;
    private Vector3 rightShoulder;
    private Vector3 leftShoulder;
    private Vector3 rightHip;
    private Vector3 leftHip;
    private Vector3 leftNeck;
    private Vector3 rightNeck;
    private Vector3 nose;
    private Vector3 neck;
    public float lengthAdjust = 0.2f;
    private Vector2 centerPoint;
    private float centerLength;
    void Start()
    {
        WebCamInPut = GameObject.Find("Visuallizer");
        
        rightHand = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rightHandTransform = rightHand.GetComponent<Transform>();
        
        leftHand = GameObject.CreatePrimitive(PrimitiveType.Cube);
        leftHandTransform = leftHand.GetComponent<Transform>();
        
        head = GameObject.CreatePrimitive(PrimitiveType.Cube);
        head.name = "head";
        headTransform = head.GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rightWrist = WebCamInPut.GetComponent<PoseVisuallizer3D>().rightWrist;
        leftWrist = WebCamInPut.GetComponent<PoseVisuallizer3D>().leftWrist;
        rightElbow = WebCamInPut.GetComponent<PoseVisuallizer3D>().rightElbow;
        leftElbow = WebCamInPut.GetComponent<PoseVisuallizer3D>().leftElbow;
        rightShoulder = WebCamInPut.GetComponent<PoseVisuallizer3D>().rightShoulder;
        leftShoulder = WebCamInPut.GetComponent<PoseVisuallizer3D>().leftShoulder;
        rightHip = WebCamInPut.GetComponent<PoseVisuallizer3D>().rightHip;
        leftHip = WebCamInPut.GetComponent<PoseVisuallizer3D>().leftHip;
        leftNeck = WebCamInPut.GetComponent<PoseVisuallizer3D>().leftNeck;
        rightNeck = WebCamInPut.GetComponent<PoseVisuallizer3D>().rightNeck;
        neck = (leftNeck + rightNeck) / 2;
        nose = WebCamInPut.GetComponent<PoseVisuallizer3D>().nose;

        centerPoint = (new Vector2(rightShoulder.x, rightShoulder.y) + new Vector2(leftShoulder.x, leftShoulder.y) + new Vector2(rightHip.x, rightHip.y) + new Vector2(leftHip.x, leftHip.y))/4;
        
        cylinder(rightHandTransform,rightElbow, rightWrist);
        cylinder(leftHandTransform,leftElbow, leftWrist);
        cylinder(headTransform,neck, nose);
        
        //test head
        // headTransform.position = neck;
        // headTransform.LookAt(nose);
        // headTransform.localScale = new Vector3(0.05f,0.05f, lengthAdjust * Mathf.Pow(2, (centerLength-0.3f) * 10));
    }

    void cylinder(Transform transform, Vector3 stratPoint, Vector3 endPoint)
    {
        // GameObject cube;
        // Transform transform;
        // cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // transform = cube.GetComponent<Transform>();
        centerLength = (new Vector2(endPoint.x, endPoint.y) - centerPoint).magnitude;

        transform.position = stratPoint;
        transform.LookAt(endPoint);
        transform.localScale = new Vector3(0.05f, 0.05f, lengthAdjust * Mathf.Pow(2, (centerLength-0.3f) * 10));
        
    }
}
//需要一个身体模型，将landmark绑定骨骼点；布置环境，编程小球运动方式。
