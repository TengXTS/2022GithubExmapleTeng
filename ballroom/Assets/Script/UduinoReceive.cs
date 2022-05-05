using TMPro;
using UnityEngine;
using Uduino;

public class UduinoReceive : MonoBehaviour
{
    private UduinoDevice uduinoBoardR;
    private UduinoDevice uduinoBoardL;
    public  float[] fingerValue = new float[10];
    public static float Remap ( float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    //You need to drag and drop this script in the callback "OnBoardConnected Event" in the Uduino inspector panel. 
    public void BoardConnected(UduinoDevice device)
    {
        // Debug.Log("Event: Board " + device.name +" connected");
        //显示text
        // GameObject text = GameObject.Find("Text (TMP)");
        // text.GetComponent<TextMeshPro>().text = "Event: Board " + device.name +" connected";

        if(device.name == "uduinoBoardR") {
            uduinoBoardR = device;
            // UduinoManager.Instance.pinMode(uduinoBoardR,39,PinMode.Input);
            UduinoManager.Instance.pinMode(uduinoBoardR,33,PinMode.Input);//小拇指
            UduinoManager.Instance.pinMode(uduinoBoardR,32,PinMode.Input);//无名指
            UduinoManager.Instance.pinMode(uduinoBoardR,35,PinMode.Input);//中指
            UduinoManager.Instance.pinMode(uduinoBoardR,34,PinMode.Input);//食指
            UduinoManager.Instance.pinMode(uduinoBoardR,39,PinMode.Input);//大拇指
        }
        if(device.name == "uduinoBoardL") {
            uduinoBoardL = device;
            // UduinoManager.Instance.pinMode(uduinoBoardL,39,PinMode.Input);
            // UduinoManager.Instance.pinMode(uduinoBoardL,39,PinMode.Input);//小拇指
            // UduinoManager.Instance.pinMode(uduinoBoardL,34,PinMode.Input);//无名指
            // UduinoManager.Instance.pinMode(uduinoBoardL,35,PinMode.Input);//中指
            // UduinoManager.Instance.pinMode(uduinoBoardL,32,PinMode.Input);//食指
            // UduinoManager.Instance.pinMode(uduinoBoardL,33,PinMode.Input);//大拇指
        }
    }

    void Update()
    {
  

        if (uduinoBoardR != null)
        {

            fingerValue[5] = Remap(UduinoManager.Instance.analogRead(uduinoBoardR,39) ,2200,0,0,10);//大拇指
            fingerValue[6] = Remap(UduinoManager.Instance.analogRead(uduinoBoardR,34) ,2000,4095,0,10);//食指
            fingerValue[7] = Remap(UduinoManager.Instance.analogRead(uduinoBoardR,35) ,1400,4095,0,10);//食指
            fingerValue[8] = Remap(UduinoManager.Instance.analogRead(uduinoBoardR,32) ,1000,3000,0,10);//食指
            fingerValue[9] = Remap(UduinoManager.Instance.analogRead(uduinoBoardR,33) ,2900,4095,0,10);//食指


            // Debug.Log("right"+UduinoManager.Instance.analogRead(uduinoBoardR,39));

        }

        if (uduinoBoardL != null)
        {
            // fingerValue[0] = Remap(UduinoManager.Instance.analogRead(uduinoBoardR,39),2100,4095,0,10);//大拇指
       
            // Debug.Log("left"+UduinoManager.Instance.analogRead(uduinoBoardR,39));
        }
    }

}