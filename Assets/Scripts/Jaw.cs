using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine; // communication with arduino

public class Jaw : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    public  float angle_l;  // left leg open angle 
    public  float angle_r;  // right leg open angle
    private float angleJaw; // whale jaw open angle

    [SerializeField] private float speed = 120f;

    // Mouth Open angleJaw Constraint
    [SerializeField] private float closeAngle = 359f;
    [SerializeField] private float openMaxAngle = 290f;
    [SerializeField] private float startingAngle = 358f;

    private float rotateAngle;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    // M0 arduino for right
    private SerialPort sp = new SerialPort("/dev/cu.usbmodem141201", 9600);

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
                //angle_l = float.Parse(sp_l.ReadLine (), System.Globalization.CultureInfo.InvariantCulture);
                //angle_r = float.Parse(sp_r.ReadLine (), System.Globalization.CultureInfo.InvariantCulture);
                string value = sp.ReadLine();
                string[] getAngle = value.Split(',');
                angle_r = float.Parse(getAngle[0]);
                angle_l = float.Parse(getAngle[1]);
                angleJaw = angle_r;

                PotentiometerControl(angleJaw);

                //Debug.Log (sp_r.ReadLine ());
                Debug.Log(angleJaw);

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