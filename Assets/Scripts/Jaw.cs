using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine; // communication with arduino

public class Jaw : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    float Angle_l;
    float Angle_r;
    float Angle;

    [SerializeField] private float speed = 120f;

    // Mouth Open Angle Constraint
    [SerializeField] private float closeAngle = 359f;
    [SerializeField] private float openMaxAngle = 290f;
    [SerializeField] private float startingAngle = 358f;

    private float rotateAngle;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    // M0 arduino for right
    SerialPort sp = new SerialPort ("/dev/cu.usbmodem141201", 9600);



    /////////////////
    /// Main Loop ///
    /////////////////

    void Start () {
        rotateAngle = 0;

        // open the event listening of the arduino port
        sp.Open ();
        sp.ReadTimeout = 1;
    }

    void Update () {
        //Debug.Log(transform.position);

        if (sp.IsOpen ) {
            try {
                //intAngle=Int32.Parse (sp.ReadLine ());
                //intAngle = sp.ReadLine ();
                //Angle_l = float.Parse(sp_l.ReadLine (), System.Globalization.CultureInfo.InvariantCulture);
                //Angle_r = float.Parse(sp_r.ReadLine (), System.Globalization.CultureInfo.InvariantCulture);
                string value = sp.ReadLine();
                string[] getAngle = value.Split(',');
                Angle_r = float.Parse(getAngle[0]);
                Angle_l = float.Parse(getAngle[1]);
                Angle = Angle_r;

                PotentiometerControl(Angle);

                //Debug.Log (sp_r.ReadLine ());
                Debug.Log(Angle);

            } catch (System.Exception) {
                //throw;
            }
        }

        KeyboardControl();
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    // control the open and close of the jaw
    // by rotating the Jaw object around the pivot located at the root of the jaw

    // ----- Keyboard Control -----

    void KeyboardControl () {
        if (Input.GetKey (KeyCode.A)) {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        } else if (Input.GetKey (KeyCode.D)) {
            transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
        }
    }

    // ----- Arduino Potentiometer Control -----

    void PotentiometerControl (float angle) {
        transform.localRotation = Quaternion.Euler(0, 0, -angle);
    }
}