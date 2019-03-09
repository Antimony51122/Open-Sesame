﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports; // communication with arduino

public class Jaw : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    // The point we want to rotate around
    public Vector3 pivotCoordinate = new Vector3(0f, 0f, -0.5f);

    [SerializeField] private float speed = 120f;

    // Mouth Open Angle Constraint
    [SerializeField] private float closeAngle    = 359f;
    [SerializeField] private float openMaxAngle  = 290f;
    [SerializeField] private float startingAngle = 358f;

    // 
    SerialPort sp = new SerialPort("COM5", 9600);

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        // open the event listening of the arduino port
        sp.Open();
        sp.ReadTimeout = 1;

        pivotCoordinate.x = transform.position.x - 1.55f;
        pivotCoordinate.y = transform.position.y + 0.15f;

        // initially set to starting angle
        transform.RotateAround(
            pivotCoordinate,
            Vector3.forward,
            startingAngle);
    }

    void Update() {
        if (sp.IsOpen) {
            try {
                PotentiometerControl(Int32.Parse(sp.ReadLine()));
                Debug.Log(Int32.Parse(sp.ReadLine()));
            }
            catch (System.Exception) {
                //throw;
            }
        }

        KeyboardControl();
        //Debug.Log(transform.localRotation.eulerAngles);

        //if (transform.localRotation.eulerAngles ). {

        //}
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    // control the open and close of the jaw
    // by rotating the Jaw object around the pivot located at the root of the jaw

    // ----- Keyboard Control -----

    void KeyboardControl() {
        if (Input.GetKey(KeyCode.A)) {
            transform.RotateAround(
                pivotCoordinate,
                Vector3.forward,
                speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.RotateAround(
                pivotCoordinate,
                Vector3.forward,
                -speed * Time.deltaTime);
        }
    }

    // ----- Arduino Potentiometer Control -----

    void PotentiometerControl(int deltaAngle) {
        // mitigate shift error which is normally between -1 to 1
        if (Math.Abs(deltaAngle) > 2) {
            transform.RotateAround(
                pivotCoordinate,
                Vector3.forward,
                -deltaAngle * 20 * Time.deltaTime);
            // 20 hahaha, just manual control factor
        }
    }
}