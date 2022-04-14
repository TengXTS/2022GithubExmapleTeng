using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicFunctions : MonoBehaviour
{
    public float[] fingers = new float[10];
    [HideInInspector]public float sliderLength = 10;    
    [Header("停止移动容差")] public float moveTolerance = 1;
    
    [Header("摄像机控制")] public float cameraHight = 0f;
    public float cameraDistance = 10f;
    
    [Header("移动速度")] public float moveSpeed = 1;
    [Header("旋转速度")] public float rotateSpeed = 5;
    
    private GameObject Myavatar;
    private GameObject myCamera;
    private Transform MyavatarTransform;

    void Start()
    {
        
        
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
    void Awake()
    {
        // DontDestroyOnLoad(this);
        Myavatar = GameObject.Find("avatar");
        myCamera = GameObject.Find("Main Camera");
        MyavatarTransform = Myavatar.GetComponent<Transform>();

    }

    void Update()
    {
        //摄像机移动
        myCamera.GetComponent<Transform>().position = new Vector3(MyavatarTransform.position.x, MyavatarTransform.position.y + cameraHight,
            MyavatarTransform.position.z - cameraDistance);
        
      
        
        

    }
    
    public void Move()
    {

        
        float moveRadial = fingers[1] - sliderLength / 2;
        float moveLateral = fingers[0] - sliderLength / 2;
        float moveNormaliazeRate;


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
        MyavatarTransform.Translate(moveLateral / 100 * moveNormaliazeRate * moveSpeed, 0, moveRadial / 100 * moveNormaliazeRate * moveSpeed);
    }
    
    public void Rotate()
    {
        float bodyRotateValue = fingers[5] - sliderLength / 2;
        if (-moveTolerance < bodyRotateValue && bodyRotateValue < moveTolerance)
        {
            bodyRotateValue = 0;
        }
        MyavatarTransform.Rotate(0f,Mathf.Deg2Rad * bodyRotateValue * rotateSpeed,0f,Space.Self);
    }
    
}
