using UnityEngine;
using System.Collections;
using Uduino;

public class UduinoReceive : MonoBehaviour
{
    // In this example, you get the connected in the device loop.
    // see MultipleArduino2 for another way to do it.
    // UduinoManager u;
    // int sensorOne = 0;
    // int sensorTwo = 0;
    // private UduinoManager manager;
    public  float[] fingerValue = new float[10];
    // private UduinoDevice firstDevice; 
    // private UduinoDevice uduinoBoardL;
    public static float Remap ( float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    void Start()
    {
        // UduinoManager.Instance.OnDataReceived += OnDataReceived;
        // UduinoDevice firstDevice = UduinoManager.Instance.GetBoard("uduinoBoardR");
        // UduinoManager.Instance.pinMode(firstDevice,39,PinMode.Input);//大拇指
        
        // uduinoBoardL = UduinoManager.Instance.GetBoard("uduinoBoardL");
        // UduinoManager.Instance.pinMode(uduinoBoardL,39,PinMode.Input);//小拇指
        
    }

    void Update()
    {
        // manager = UduinoManager.Instance;
        GameObject eventDontWant = GameObject.Find("Uduino/UduinoInterface/EventSystem");
        Destroy(eventDontWant);
        
        
        if (UduinoManager.Instance.hasBoardConnected())
        {
            
            UduinoDevice firstDevice = UduinoManager.Instance.GetBoard("uduinoBoardR");
            //right
            UduinoManager.Instance.pinMode(firstDevice,33,PinMode.Input);//小拇指
            fingerValue[9] = Remap(UduinoManager.Instance.analogRead(firstDevice,33),2100,4095,0,10);
            UduinoManager.Instance.pinMode(firstDevice,32,PinMode.Input);//无名指
            fingerValue[8] = Remap(UduinoManager.Instance.analogRead(firstDevice,32),0,2600,0,10);
            UduinoManager.Instance.pinMode(firstDevice,35,PinMode.Input);//中指
            fingerValue[7] = Remap(UduinoManager.Instance.analogRead(firstDevice,35) ,1100,4095,0,10);
            UduinoManager.Instance.pinMode(firstDevice,34,PinMode.Input);//食指
            fingerValue[6] = Remap(UduinoManager.Instance.analogRead(firstDevice,34) ,1500,4095,0,10);
            UduinoManager.Instance.pinMode(firstDevice,39,PinMode.Input);//大拇指
            fingerValue[5] = Remap(UduinoManager.Instance.analogRead(firstDevice,39) ,1500,0,0,10);
            
            Debug.Log("右手大拇指"+fingerValue[5]);
            Debug.Log("右手食指"+fingerValue[6]);
            Debug.Log("右手中指"+fingerValue[7]);
            Debug.Log("右手无名指"+fingerValue[8]);
            Debug.Log("右手小指"+fingerValue[9]);
            
            
            
            
            // UduinoDevice secondDevice = UduinoManager.Instance.GetBoard("uduinoBoardL");

            //left
            // UduinoManager.Instance.pinMode(secondDevice,39,PinMode.Input);//小拇指
            // fingerValue[4] = UduinoManager.Instance.analogRead(secondDevice,39);
            // UduinoManager.Instance.pinMode(uduinoBoardL,34,PinMode.Input);//无名指
            // fingerValue[3] = UduinoManager.Instance.analogRead(uduinoBoardL,34);
            // UduinoManager.Instance.pinMode(uduinoBoardL,35,PinMode.Input);//中指
            // fingerValue[2] = UduinoManager.Instance.analogRead(uduinoBoardL,35);
            // UduinoManager.Instance.pinMode(Left,32,PinMode.Input);//食指
            // fingerValue[1] = UduinoManager.Instance.analogRead(Left,32);
            // UduinoManager.Instance.pinMode(Left,33,PinMode.Input);//大拇指
            // fingerValue[0] = UduinoManager.Instance.analogRead(Left,33);
            
          
            // UduinoManager.Instance.sendCommand(Left, "GetVariable");
            // UduinoManager.Instance.sendCommand(Right, "GetVariable");
            // Debug.Log("Variable of the first board:" + sensorOne);
            // Debug.Log("Variable of the second board:" + sensorTwo);
        }
        // else
        // {
        //     Debug.Log("The boards have not been detected");
        // }
    }

    // void OnDataReceived(string data, UduinoDevice device)
    // {
    //     if (device.name == "uduinoBoardR")
    //     {
            // sensorOne = int.Parse(data);
            // manager.pinMode(Right,33,PinMode.Input);//小拇指
            // fingerValue[9] = manager.analogRead(Right,33);
            // manager.pinMode(Right,32,PinMode.Input);//无名指
            // fingerValue[8] = manager.analogRead(Right,32);
            // manager.pinMode(Right,35,PinMode.Input);//中指
            // fingerValue[7] = manager.analogRead(Right,35);
            // manager.pinMode(Right,34,PinMode.Input);//食指
            // fingerValue[6] = manager.analogRead(Right,34);
            // manager.pinMode(Right,39,PinMode.Input);//大拇指
            // fingerValue[5] = manager.analogRead(Right,39);
        // }
        // else if (device.name == "uduinoBoardL")
        // {
            // sensorTwo = int.Parse(data);
            // manager.pinMode(39,PinMode.Input);//小拇指
            // fingerValue[4] = manager.analogRead(39);
            // manager.pinMode(34,PinMode.Input);//无名指
            // fingerValue[3] = manager.analogRead(34);
            // manager.pinMode(35,PinMode.Input);//中指
            // fingerValue[2] = manager.analogRead(35);
            // manager.pinMode(32,PinMode.Input);//食指
            // fingerValue[1] = manager.analogRead(32);
            // manager.pinMode(33,PinMode.Input);//大拇指
            // fingerValue[0] = manager.analogRead(33);
        // }
        
    // }
}