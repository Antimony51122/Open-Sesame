using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine; // communication with arduino

// TODO: only eat when mouth is opened

public class Jaw : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private bool isRightLeg;

    private float angleJaw; // whale jaw open angle controlled by leg open angle

    [SerializeField] private float speed = 120f;

    // Mouth Open angleJaw Constraint
    [SerializeField] private float closeAngle = 359f;
    [SerializeField] private float openMaxAngle = 290f;
    [SerializeField] private float startingAngle = 358f;

    private float rotateAngle;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    [SerializeField] private GameObject arduinoGameObjectGame;
    private ArduinoHelper arduinoHelper;

    private CalibrationMenu calibrationMenu;

    /////////////////
    /// Main Loop ///
    /////////////////

    void Awake() {
        arduinoHelper = arduinoGameObjectGame.GetComponent<ArduinoHelper>();
        calibrationMenu = new CalibrationMenu();
    }

    void Start () {
        // set the jaw initial rotation angle to 0
        rotateAngle = 0;
    }

    void Update () {
        //Debug.Log(transform.position);
        
        // 60 degree max in game --> map to --> user input range
        // Controlling Jaw by accessing to the right leg angle of the Serial output
        Debug.Log(calibrationMenu.angleRightConstraint);
        
        if (isRightLeg) {
            angleJaw = arduinoHelper.angle_r / (calibrationMenu.angleRightConstraint / 60f);
        }
        else {
            angleJaw = arduinoHelper.angle_l / (calibrationMenu.angleRightConstraint / 60f);
        }

        PotentiometerControl(angleJaw);

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