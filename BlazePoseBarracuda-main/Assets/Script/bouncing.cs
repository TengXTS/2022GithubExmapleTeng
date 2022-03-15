using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class bouncing : MonoBehaviour
{
    // Start is called before the first frame update
    // private Shader shaderBall;
    // private Material materialBall;
    [Range(0.0f, 1.0f)]
    public float bounciness = 0.6f;
    public bool doubleBounciness = false;
    [Range(0.0f, 1.0f)]
    public float mass = 0.5f;

    [Header("Up Force Range")] 
    public float upForceMin = 10;
    public float upForceMax = 15;
    [Space(10)]

    public float decreaseUpForce = 0.5f;
    public float sphereDiameter = 1;
    private float[] upForce = new float[100];
    private float[] upForceUpdate = new float[100];

    private bool ifMouseHold;
    private bool first = true;
    GameObject[] ball = new GameObject[100];
    
    public Texture bouncingTex;
    public Texture planeTex;
    private GameObject poseDetectOject;
    



    
    void Start()
    {
        poseDetectOject = GameObject.FindGameObjectWithTag("webcam");

        
        PhysicMaterial bounce = new PhysicMaterial();
        bounce.bounciness = bounciness;
        GameObject floor = new GameObject();
        floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.GetComponent<Transform>().position = new Vector3(0, -sphereDiameter * 2.5f, 0);
        floor.GetComponent<Transform>().localScale = new Vector3(sphereDiameter * 10, 1, sphereDiameter * 10);
        floor.isStatic = true;
        if (doubleBounciness == true)
        {
            floor.GetComponent<Collider>().material = bounce;
        }

        for(int i = 1; i < 10; i++)
        {
            for (int j = 1; j < 10; j++)
            {
                int index = i * 10 + j;
                ball[index] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                ball[index].name = "ball" + index.ToString();
                // ball[i].AddComponent<MeshRenderer>();
                ball[index].GetComponent<Collider>().material = bounce;
                ball[index].GetComponent<Transform>().position = new Vector3(i * sphereDiameter * 2 - sphereDiameter * 10, -sphereDiameter * 2, j * sphereDiameter * 2);
                ball[index].GetComponent<Transform>().localScale = new Vector3(sphereDiameter, sphereDiameter, sphereDiameter);
                ball[index].AddComponent<Rigidbody>();
                ball[index].GetComponent<Rigidbody>().mass = mass;
                ball[index].GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
                upForce[index] = 0;
                ball[index].AddComponent<ConstantForce>().force = new Vector3(0, 0, 0);
                // ball[index].GetComponent<Rigidbody>().AddForce(new Vector3(0, upForce[index], 0));

                
            }
        }
    }


    // private void OnGUI()
    // {
    //     //activate bouncing mode
    //     if (GUI.Button(new Rect(10, 10, 50, 50), bouncingTex))
    //     {
    //         Debug.Log("bouncing");
    //         if (poseDetectOject == null)
    //         {
    //             Debug.Log("null");
    //         }
    //
    //         poseDetectOject.SetActive(false);
    //
    //
    //     }
    //     //activate 2D mode
    //     if (GUI.Button(new Rect(10, 10, 50, 50), planeTex))
    //     {
    //         Debug.Log("Clicked the button with an image");
    //     }
    // }


 

    void Update()
    {
        
        OnlyRandomTheUpForceOncePerMouseHolding();

        
        
        for (int i = 1; i < 10; i++)
        {
            for (int j = 1; j < 10; j++)
            {
                int index = i * 10 + j;
                if (upForce[index] <= 9.8)
                {
                    upForce[index] = 0;
                }
                else
                {
                    upForce[index] = upForce[index] - decreaseUpForce;
                }

                if (ifMouseHold == true)
                {
                    upForce[index] = upForceUpdate[index];
                    // Debug.Log("hold");

                }

                // Debug.Log(upForce[index]);
                ball[index].GetComponent<ConstantForce>().force = new Vector3(0, upForce[index], 0);
                // ball[index].GetComponent<Rigidbody>().AddForce(new Vector3(0, upForce[index], 0));

            }
        }
        
        
        
    }

    void OnlyRandomTheUpForceOncePerMouseHolding()
    {
        if(Input.GetMouseButton(0))
        {
            ifMouseHold = true;
        }
        else
        {
            ifMouseHold = false;
            first = true;
        }

        if (ifMouseHold == true && first == true)
        {
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    int index = i * 10 + j;
                    upForceUpdate[index] = Random.Range(upForceMin,upForceMax);
                    // Debug.Log("random");
                }
            }
            first = false;
        }
    }
    
    
    
}
