using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Uduino;

public class UduinoReceive2 : MonoBehaviour
{
    private UduinoManager manager;
    // [HideInInspector]
    public  float[] fingerValue = new float[10];

    private void Start()
    {
        manager = UduinoManager.Instance;
        GameObject eventDontWant = GameObject.Find("Uduino/UduinoInterface/EventSystem");
        Destroy(eventDontWant);
    }

    private void Update()
    {
        //right
        manager.pinMode(33,PinMode.Input);//大拇指
        fingerValue[5] = manager.analogRead(33) / 1024f;
        manager.pinMode(25,PinMode.Input);//食指
        fingerValue[6] = manager.analogRead(25) / 1024f;
        manager.pinMode(26,PinMode.Input);//中指
        fingerValue[7] = manager.analogRead(26) / 1024f;
        manager.pinMode(27,PinMode.Input);//无名指
        fingerValue[8] = manager.analogRead(27) / 1024f;
        manager.pinMode(14,PinMode.Input);//小拇指
        fingerValue[9] = manager.analogRead(39) / 1024f;
        
        //left
        // manager.pinMode(33,PinMode.Input);//大拇指
        // fingerValue[0] = manager.analogRead(33) / 1024f;
        // manager.pinMode(32,PinMode.Input);//食指
        // fingerValue[1] = manager.analogRead(32) / 1024f;
        // manager.pinMode(35,PinMode.Input);//中指
        // fingerValue[2] = manager.analogRead(35) / 1024f;
        // manager.pinMode(34,PinMode.Input);//无名指
        // fingerValue[3] = manager.analogRead(34) / 1024f;
        // manager.pinMode(39,PinMode.Input);//小拇指
        // fingerValue[4] = manager.analogRead(39) / 1024f;
        
      
    }
}