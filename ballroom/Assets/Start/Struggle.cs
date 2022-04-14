using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Struggle : MonoBehaviour
{
    private float[] fingers = new float[10];
    private float sliderLength;
    private PublicFunctions publicFunctions;
    
    //挣扎判定
    private bool[,] marks = new bool[10,3];
    private bool[] marksFinal = {false,false,false,false,false,false,false,false,false,false};
    private bool[] marksFinalTrue = {true, true,true, true,true, true,true, true,true, true};




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
        
    }
    
    // Update is called once per frame
    void Update()
    {
       
        
//挣扎判定main。目前是转两圈，如果要加圈数要全部改。
        for (int i = 0; i < 10; i++)
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

        if (marksFinal[0] == true)
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            publicFunctions.Move();
            publicFunctions.Rotate();
            // Move();
            // Rotate();
        }
        

        
    }
    

}
