using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Struggle : MonoBehaviour
{
    [Range(0,1)]
    public float floatSpeed = 0.2f;
    private float[] fingers = new float[10];
    private float sliderLength;
    private PublicFunctions publicFunctions;
    
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
    
    // Update is called once per frame
    void Update()
    {

        GameObject LeftForeArm = GameObject.Find("LeftForeArm__13");
        LeftForeArm.GetComponent<Transform>().Rotate(Mathf.Deg2Rad * (fingers[6] - sliderLength / 2), 0f, 0f, Space.Self);
           
        
        //摄像机
        StartCoroutine(ExampleCoroutine());

        cameraHight += cameraHightSpeed;
        cameraDistance += cameraDistanceSpeed;
        
        XROrinign.GetComponent<Transform>().position = new Vector3(MyavatarTransform.position.x, MyavatarTransform.position.y + cameraHight,
            MyavatarTransform.position.z + cameraDistance);
        if (cameraHight >= 2)
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
        yield return new WaitForSeconds(2f);
        cameraHightSpeed += 0.000125f;
        cameraDistanceSpeed += 0.0005f;
    }
    

}
