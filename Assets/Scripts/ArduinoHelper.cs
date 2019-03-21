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

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    // M0 arduino
    private SerialPort sp = new SerialPort("COM5", 9600);

    /////////////////
    /// Main Loop ///
    /////////////////
    void Start() {
        // open the event listening of the arduino port
        sp.Open();
        sp.ReadTimeout = 1;
    }

    // Update is called once per frame
    void Update() {
        if (sp.IsOpen) {
            try {
                string value = sp.ReadLine();
                string[] getAngle = value.Split(',');
                angle_r = float.Parse(getAngle[0]);
                angle_l = float.Parse(getAngle[1]);
            } catch (System.Exception) {
                //throw;
            }
        }
    }
}