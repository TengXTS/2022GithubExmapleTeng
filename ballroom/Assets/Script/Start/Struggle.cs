using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;


public class Struggle : MonoBehaviour
{
    
    [Range(0,1)]
    public float floatSpeed = 0.2f;
    private float[] fingers = new float[10];
    private float sliderLength;
    private PublicFunctions publicFunctions;
    
    //控制大手骨骼
    public float fingerSpeed = 1f;
    public float handSpeed = 1f;
    public float armRollSpeed = 1f;
    public float armSpeed = 1f;


    
    //挣扎判定
    private bool[,] marks = new bool[10,3];
    private bool[] marksFinal = {false,false,false,false,false,false,false,false,false,false};
    private bool[] marksFinalTrue = {true, true,true, true,true, true,true, true,true, true};

    private bool ifInLight;
    private bool ifFloat = false;
    
    
    private GameObject Myavatar;
    private GameObject myCamera;
    private GameObject XROrinign;
    private Transform MyavatarTransform;
    private float cameraHight = -1f;
    private float cameraHightSpeed = 0.0003f;
    private float cameraDistance = -11f;
    private float cameraDistanceSpeed = 0.0003f;
    
  
    
    // class HandRotate
    // //slerp可以很好地限制角度，但是要取得：通过将一根大拇指的合理范围（两个world四元数）转化为local数值，打印出这个数值并手动输入start和end。slerp后，再乘每个大拇指的local向上方向的world数值，
    // {
    //     static float pointer = 0.5f;
    //   
    //     public static void Rotate(string tag, float fingerIndex, int speed, Quaternion startQuaternion, Quaternion endQuaternion, float sliderLength)
    //     {
    //         foreach (GameObject finger in GameObject.FindGameObjectsWithTag(tag))
    //         {
    //             Transform fingerTrans = finger.GetComponent<Transform>();
    //
    //             // const Quaternion localUp = fingerTrans.rotation;
    //             pointer += (fingerIndex - sliderLength / 2) / sliderLength / speed;
    //             pointer = Mathf.Clamp(pointer,0,1);
    //             Quaternion slerp = Quaternion.Slerp(startQuaternion, endQuaternion, pointer);
    //             fingerTrans.rotation = Quaternion.Euler(fingerTrans.up) * slerp;
    //             Debug.Log(finger.name + fingerTrans.up);
    //         }
    //
    //     }
    //
    // }

    void Start()
    {
        publicFunctions = GameObject.Find("Script").GetComponent<PublicFunctions>();
        
        fingers = publicFunctions.fingers;
        sliderLength = publicFunctions.sliderLength;
   
        //挣扎判定，
        for(int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                marks[i, j] = false;
            }
            //手指中位
            fingers[i] = sliderLength / 2;

        }
        
        Myavatar = GameObject.Find("avatar");
        myCamera = GameObject.Find("Main Camera");
        XROrinign = GameObject.Find("XR Origin");
        MyavatarTransform = Myavatar.GetComponent<Transform>();
        
       
        
    }

    void HandRotate(string tag, float FingerValue, float speed)
    {
        foreach (GameObject finger in GameObject.FindGameObjectsWithTag(tag))
        {
            Transform fingerTrans = finger.GetComponent<Transform>();
            fingerTrans.RotateAround(fingerTrans.position, -fingerTrans.up,
                (FingerValue - sliderLength / 2) * Time.deltaTime * speed);
        }
    }


    

    void Update()
    {


        //地面手运动
        // foreach (GameObject finger in GameObject.FindGameObjectsWithTag("thumb"))
        // {
        //     Transform fingerTrans = finger.GetComponent<Transform>();
        //     fingerTrans.RotateAround(fingerTrans.position, -fingerTrans.up,
        //         (fingers[5] - sliderLength / 2) * Time.deltaTime * 10 );
            // Quaternion referQ = fingerTrans.parent.GetComponent<Transform>().rotation;
            // float degree = Quaternion.Angle(fingerTrans.rotation, referQ);
        // }
        // HandRotate.Rotate("thumb",fingers[5],5000,new Quaternion(0.252590299f,-0.197640359f,-0.559742451f,0.764084399f),new Quaternion(0.511026144f,0.237734988f,-0.340537012f,0.752574801f),sliderLength);
        
        HandRotate("thumb", fingers[5], fingerSpeed);
        HandRotate("index", fingers[6], fingerSpeed);
        HandRotate("middle", fingers[7], fingerSpeed);
        HandRotate("ring", fingers[8], fingerSpeed);
        HandRotate("pinky", fingers[9], fingerSpeed);
        HandRotate("hand", fingers[5], handSpeed);
        HandRotate("armRoll", fingers[7], armRollSpeed);
        HandRotate("arm", fingers[9], armSpeed);


        //摄像机//前三行为是否向前推进
        StartCoroutine(ExampleCoroutine());

        cameraHight += cameraHightSpeed;
        cameraDistance += cameraDistanceSpeed;
        
        XROrinign.GetComponent<Transform>().position = new Vector3(MyavatarTransform.position.x, MyavatarTransform.position.y + cameraHight,
            MyavatarTransform.position.z + cameraDistance);
        if (cameraHight >= 1.3)
        {
            StopAllCoroutines();
            cameraHightSpeed = 0;
            cameraDistanceSpeed = 0;

        }
        
        
//挣扎判定main。目前是转两圈，如果要加圈数要全部改。
        for (int i = 5; i < 10; i++)
        {
            if(fingers[i] >= sliderLength - 0.5 )
            {
                marks[i,0] = true;
            }
            if((fingers[i] <= 0.5) && (marks[i,0] == true) )
            {
                marks[i,1] = true;
            }

            if ((fingers[i] >= sliderLength - 0.5) && (marks[i,1] == true))
            {
                marks[i,2] = true; 
            }

            if ((fingers[i] <= 0.5) && (marks[i,2] == true))
            {
                marksFinal[i] = true;
                // Debug.Log(i);
            }
        }
        if (marksFinal.SequenceEqual(marksFinalTrue))
        {
            Debug.Log("Complete struggle");

        }

        if (marksFinal[5] == true && ifFloat == false)//这个index指具体哪个手指
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            publicFunctions.Move("Walk");
            // publicFunctions.Rotate();//不接受数字时使用rotation会导致转圈。使用rotation暂时不使用
            // Move();
            // Rotate();
        }
        
        //光中漂浮
        ifInLight = GameObject.Find("LightColiider").GetComponent<FloatInLight>().ifInLight;
        if (ifInLight == true)
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            this.GetComponent<Rigidbody>().AddForce(0,floatSpeed,0);
            ifFloat = true;
            // this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            // this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            // publicFunctions.Float();
            // Debug.Log("1");
            
        }
        

        
    }
    
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(4f);
        cameraHightSpeed += 0.000125f;
        cameraDistanceSpeed += 0.0007f;
    }
    

}
