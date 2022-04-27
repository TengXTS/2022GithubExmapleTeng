using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class UduinoReceive3 : MonoBehaviour
{
    UduinoManager u;
    int sensorOne = 0;
    int sensorTwo = 0;
    public  float[] fingerValue = new float[10];


    UduinoDevice Right = null;
    UduinoDevice Left = null;

    void Start()
    {
        UduinoManager.Instance.OnBoardConnected += OnBoardConnected;
        UduinoManager.Instance.OnDataReceived += OnDataReceived;
    }

    void Update()
    {
        if(Right != null)
            UduinoManager.Instance.sendCommand(Right, "GetVariable");
        if (Left != null)
            UduinoManager.Instance.sendCommand(Left, "GetVariable");
        Debug.Log("Variable of the first board:" + sensorOne);
        Debug.Log("Variable of the second board:" + sensorTwo);
    }

    void OnDataReceived(string data, UduinoDevice device)
    {
        if (device.name == "uduinoBoardR") sensorOne = int.Parse(data);
        else if (device.name == "uduinoBoardL") sensorTwo = int.Parse(data);
    }

    // Different setups for each arduino board
    void OnBoardConnected(UduinoDevice connectedDevice)
    {
        //You can launch specific functions here
        if (connectedDevice.name == "uduinoBoardR")
        {
            Right = connectedDevice;
            UduinoManager.Instance.pinMode(Right,33,PinMode.Input);//小拇指
            fingerValue[9] = UduinoManager.Instance.analogRead(Right,33);
            UduinoManager.Instance.pinMode(Right,32,PinMode.Input);//无名指
            fingerValue[8] = UduinoManager.Instance.analogRead(Right,32);
            UduinoManager.Instance.pinMode(Right,35,PinMode.Input);//中指
            fingerValue[7] = UduinoManager.Instance.analogRead(Right,35);
            UduinoManager.Instance.pinMode(Right,34,PinMode.Input);//食指
            fingerValue[6] = UduinoManager.Instance.analogRead(Right,34);
            UduinoManager.Instance.pinMode(Right,39,PinMode.Input);//大拇指
            fingerValue[5] = UduinoManager.Instance.analogRead(Right,39);
        }
        else if (connectedDevice.name == "uduinoBoardL")
        {
            Left = connectedDevice;
            UduinoManager.Instance.pinMode(Left,39,PinMode.Input);//小拇指
            fingerValue[4] = UduinoManager.Instance.analogRead(Left,39);
            UduinoManager.Instance.pinMode(Left,34,PinMode.Input);//无名指
            fingerValue[3] = UduinoManager.Instance.analogRead(Left,34);
            UduinoManager.Instance.pinMode(Left,35,PinMode.Input);//中指
            fingerValue[2] = UduinoManager.Instance.analogRead(Left,35);
            UduinoManager.Instance.pinMode(Left,32,PinMode.Input);//食指
            fingerValue[1] = UduinoManager.Instance.analogRead(Left,32);
            UduinoManager.Instance.pinMode(Left,33,PinMode.Input);//大拇指
            fingerValue[0] = UduinoManager.Instance.analogRead(Left,33);
        }
    }
}
