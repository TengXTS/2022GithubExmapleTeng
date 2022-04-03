using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEditor.TextCore.Text;
using UnityEngine;
using Random=UnityEngine.Random;

public class fingervalues : MonoBehaviour
{
    
    public float[] fingers;


    [Header("摄像机控制")]
    public float cameraHight = 2.5f;
    public float cameraDistance = 2f;
    [Header("停止移动容差")]
    public float moveTolerance = 1;
    [Header("手脚转动范围")]
    public float handAngle = 180;
    public float footAngle = 90;
    [Header("各肢体起始角度")]
    public float leftHandAngleAdjust = 90;
    public float rightHandAngleAdjust = -90;
    public float leftFootAngleAdjust = 45;
    public float rightFootAngleAdjust = 45;
    [Header("肢体长度（暂时）")] 
    public float limbLength = 1;
    
    //instance相关
    public Mesh mesh;
    public Material material;
    private Matrix4x4[] matrices;
    private MaterialPropertyBlock block;
    private int amount;
    public float meshScale = 0.1f;
    private Vector3[] verticesPosition;
    private float sliderLength = 10;
    private float floatValue;
    
    private GameObject leftHand;
    private GameObject leftShoulder;
    private Transform leftHandTransform;
    private Transform leftShoulderTransform;
    private Vector3 leftShoulderPosition;
    
    private GameObject rightHand;
    private GameObject rightShoulder;
    private Transform rightHandTransform;
    private Transform rightShoulderTransform;
    private Vector3 rightShoulderPosition;
    
    private float leftHandAngle;
    private float rightHandAngle;
    
    private GameObject leftFoot;
    private GameObject leftHip;
    private Transform leftFootTransform;
    private Transform leftHipTransform;
    private Vector3 leftHipPosition;
    
    private GameObject rightFoot;
    private GameObject rightHip;
    private Transform rightFootTransform;
    private Transform rightHipTransform;
    private Vector3 rightHipPosition;

    private float leftFootAngle;
    private float rightFootAngle;

    private GameObject Myavatar;
    private GameObject pCube1;
    private Transform MyavatarTransform;
    private float moveRadial;
    private float moveLateral;
    private float rotateValue;
    private float moveNormaliazeRate;

    
    public static float Remap ( float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    void Start()
    {
        fingers = new float[10];
        
        leftHand = GameObject.Find("leftHand");
        leftHandTransform = leftHand.GetComponent<Transform>();
        leftShoulder = GameObject.Find("leftShoulder");
        leftShoulderTransform = leftShoulder.GetComponent<Transform>();
        
        rightHand = GameObject.Find("rightHand");
        rightHandTransform = rightHand.GetComponent<Transform>();
        rightShoulder = GameObject.Find("rightShoulder");
        rightShoulderTransform = rightShoulder.GetComponent<Transform>();
        
        leftFoot = GameObject.Find("leftFoot");
        leftFootTransform = leftFoot.GetComponent<Transform>();
        leftHip = GameObject.Find("leftHip");
        leftHipTransform = leftHip.GetComponent<Transform>();
        
        rightFoot = GameObject.Find("rightFoot");
        rightFootTransform = rightFoot.GetComponent<Transform>();
        rightHip = GameObject.Find("rightHip");
        rightHipTransform = rightHip.GetComponent<Transform>();
        
        Myavatar = GameObject.Find("avatar");
        MyavatarTransform = Myavatar.GetComponent<Transform>();
        
        fingers[0] = sliderLength / 2;
        fingers[1] = sliderLength / 2;
        fingers[2] = sliderLength / 2;
        fingers[3] = sliderLength / 2;
        fingers[4] = sliderLength / 2;
        fingers[5] = sliderLength / 2;
        fingers[7] = sliderLength / 2;
        fingers[8] = sliderLength / 2;

        pCube1 = GameObject.Find("pCube1");
        block = new MaterialPropertyBlock();
        amount = pCube1.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertexCount;
        matrices = new Matrix4x4[amount];
        verticesPosition = new Vector3[amount];

        verticesPosition = pCube1.GetComponent<SkinnedVertices>().verticesPosition;
        
    }
    void OnGUI()
    {
        //left
        for (int i = 0; i < 5; i++)
        {
            fingers[i] = GUI.HorizontalSlider(new Rect(25, 25*(i+1), 100, 30), fingers[i], 0.0F, sliderLength);
        }
        //right
        for (int i = 5; i < 10; i++)
        {
            fingers[i] = GUI.HorizontalSlider(new Rect(150, 25*(i-4), 100, 30), fingers[i], 0.0F, sliderLength);
        }

    }



    void Update()
    {
        LimbControl();
        Float();
        Move();
        Rotate();
        AddElements();
        // Debug.Log(abc.verticesPosition1[0]);




        //摄像机移动
        transform.position = new Vector3(MyavatarTransform.position.x, MyavatarTransform.position.y + cameraHight,
            MyavatarTransform.position.z - cameraDistance);

    }


    private void AddElements()
    {
        //身上聚合一些元素
        for (int i = 0; i < amount; i++)
        {
            // Quaternion rotation = Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180, 180), Random.Range(-180, 180));
            Quaternion rotation = new Quaternion(1,0,0,0);
            Vector3 scale = Vector3.one * meshScale;
            matrices[i] = Matrix4x4.TRS(verticesPosition[i], rotation, scale);
        }

        Graphics.DrawMeshInstanced(mesh, 0, material, matrices, amount, block);
        //随机大小，顺肢体方向的旋转
        //更改一个更合适的模型，最好是有手肘膝盖的。
    }


    void LimbControl()
    {
        //最好增加一些变化
        leftHandAngle = fingers[6]*(handAngle / sliderLength) + leftHandAngleAdjust;
        leftShoulderPosition = leftShoulderTransform.position;
        leftHandTransform.position = new Vector3( leftShoulderPosition.x + limbLength * Mathf.Cos(Mathf.Deg2Rad * leftHandAngle),leftShoulderPosition.y - limbLength * Mathf.Sin(Mathf.Deg2Rad * leftHandAngle), leftShoulderPosition.z);
        
        rightHandAngle = fingers[9]*(handAngle / sliderLength) + rightHandAngleAdjust;
        rightShoulderPosition = rightShoulderTransform.position;
        rightHandTransform.position = new Vector3( rightShoulderPosition.x + limbLength * Mathf.Cos(Mathf.Deg2Rad * rightHandAngle),rightShoulderPosition.y + limbLength * Mathf.Sin(Mathf.Deg2Rad * rightHandAngle),rightShoulderPosition.z);
        
        leftFootAngle = fingers[7]*(footAngle / sliderLength) + leftFootAngleAdjust;
        leftHipPosition = leftHipTransform.position;
        leftFootTransform.position = new Vector3( leftHipPosition.x,leftHipPosition.y - limbLength * Mathf.Sin(Mathf.Deg2Rad * leftFootAngle), leftHipPosition.z  + limbLength * Mathf.Cos(Mathf.Deg2Rad * leftFootAngle));

        rightFootAngle = fingers[8]*(footAngle / sliderLength) + rightFootAngleAdjust;
        rightHipPosition = rightHipTransform.position;
        rightFootTransform.position = new Vector3( rightHipPosition.x,rightHipPosition.y - limbLength * Mathf.Sin(Mathf.Deg2Rad * rightFootAngle), rightHipPosition.z  + limbLength * Mathf.Cos(Mathf.Deg2Rad * rightFootAngle));
    }

    void Float()
    {
        floatValue = (fingers[2] + fingers[3] + fingers[4]) / 3 - sliderLength / 2;
        
        if (-moveTolerance / 3 < floatValue && floatValue < moveTolerance / 3)
        {
            floatValue = 0;
        }
        MyavatarTransform.Translate(0,floatValue / 100, 0);
    }

    void Move()
    {
        moveRadial = fingers[1] - sliderLength / 2;
        // moveRadial = Remap(moveRadial, -5, 5, -20, 20);
        moveLateral = fingers[0] - sliderLength / 2;

        if (-moveTolerance  < moveLateral && moveLateral < moveTolerance)
        {
            moveLateral = 0;
        }
        if (-moveTolerance < moveRadial && moveRadial < moveTolerance)
        {
            moveRadial = 0;
        }
        if (!((moveLateral == 0) && (moveRadial == 0)))
        {
            moveNormaliazeRate = 1 / Mathf.Sqrt(Mathf.Pow(moveLateral, 2) + Mathf.Pow(moveRadial, 2));
        }
        else
        {
            moveNormaliazeRate = 0;
        }
        MyavatarTransform.Translate(moveLateral / 100 * moveNormaliazeRate, 0, moveRadial / 100 * moveNormaliazeRate);
    }
    
    void Rotate()
    {
        rotateValue = fingers[5] - sliderLength / 2;
        if (-moveTolerance < rotateValue && rotateValue < moveTolerance)
        {
            rotateValue = 0;
        }
        MyavatarTransform.Rotate(0f,Mathf.Deg2Rad * rotateValue,0f,Space.Self);
    }
}





