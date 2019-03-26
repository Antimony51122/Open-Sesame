using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine; // communication with arduino

public class ArduinoHelper : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    public float angle_l; // left leg open angle 
    public float angle_r; // right leg open angle
    public int buttonNum; // binary between 0 and 1 from the button output

    public string path; // the file address the angle data will be written into

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    private WriteTextHelper writeTextHelper;

    // M0 Arduino
    public SerialPort sp = new SerialPort("COM5", 9600);

    /////////////////
    /// Main Loop ///
    /////////////////

    void Start() {
        writeTextHelper = new WriteTextHelper();

        // open the event listening of the arduino port
        sp.Open();
        sp.ReadTimeout = 1;

        // need to re-assign the path variable or otherwise will encounter ArgumentNullException
        path = "C:/Users/HRK/Desktop/Angle/test.txt";
    }

    void Update() {
        if (sp.IsOpen) {
            try {
                string value = sp.ReadLine();
                string[] getValue = value.Split(',');
                angle_r   = float.Parse(getValue[0]);
                angle_l   = float.Parse(getValue[1]);
                buttonNum = int.Parse(getValue[2]);
                //Debug.Log(buttonNum);
                writeTextHelper.WriteString(getValue[0], getValue[1]);
            } catch (System.Exception) {
                //throw;
            }
        }
    }
}