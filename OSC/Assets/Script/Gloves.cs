using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloves : MonoBehaviour
{
    [Header("Left Hand")]
    public int leftPinky;
    public int leftRing;
    public int leftMiddle;
    public int leftIndex;
    public int leftThumb;
    
    [Header("Right Hand")]
    public int rightPinky;
    public int rightRing;
    public int rightMiddle;
    public int rightIndex;
    public int rightThumb;
    

    
    public int LeftPinky
    {
        get
        {
            return leftPinky;
        }
        set
        {
            leftPinky = value;
        }
    }
    
    public int LeftRing
    {
        get
        {
            return leftRing;
        }
        set
        {
            leftRing = value;
        }
    }
    
    public int LeftMiddle
    {
        get
        {
            return leftMiddle;
        }
        set
        {
            leftMiddle = value;
        }
    }
    
    public int LeftIndex
    {
        get
        {
            return leftIndex;
        }
        set
        {
            leftIndex = value;
        }
    }
    
    public int LeftThumb
    {
        get
        {
            return leftThumb;
        }
        set
        {
            leftThumb = value;
        }
    }

    public int RightPinky
    {
        get { return rightPinky; }
        set { rightPinky = value; }
    }

    public int RightRing
    {
        get { return rightRing; }
        set { rightRing = value; }
    }

    public int RightMiddle
    {
        get { return rightMiddle; }
        set { rightMiddle = value; }
    }

    public int RightIndex
    {
        get { return rightIndex; }
        set { rightIndex = value; }
    }

    public int RightThumb
    {
        get { return rightThumb; }
        set { rightThumb = value; }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetRightFingers(string data)
    {
        var tokens = data.Split(";");
        if (tokens.Length != 5)
        {
            Debug.Log("Invalid number of values for right Hand! Need 5 got " + tokens.Length);
            return;
        }

        rightPinky = int.Parse(tokens[0]);
        rightRing = int.Parse(tokens[1]);
        rightMiddle = int.Parse(tokens[2]);
        rightIndex = int.Parse(tokens[3]);
        rightThumb = int.Parse(tokens[4]);
    }

    public void SetLeftFingers(string data)
    {
        var tokens = data.Split(";");
        if (tokens.Length != 5)
        {
            Debug.Log("Invalid number of values for left Hand! Need 5 got " + tokens.Length);
            return;
        }

        leftPinky = int.Parse(tokens[0]);
        leftRing = int.Parse(tokens[1]);
        leftMiddle = int.Parse(tokens[2]);
        leftIndex = int.Parse(tokens[3]);
        leftThumb = int.Parse(tokens[4]);
    }
}
