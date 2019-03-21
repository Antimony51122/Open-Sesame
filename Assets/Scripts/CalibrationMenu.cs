﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationMenu : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    private float angleLeft;
    private float angleRight;

    // the 2 angle constraints will be accessed by the Jaw class
    public float angleLeftConstraint;
    public float angleRightConstraint;

    [SerializeField] private Text angleLeftText;
    [SerializeField] private Text angleRightText;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    private ArduinoHelper arduinoHelper;

    /////////////////
    /// Main Loop ///
    /////////////////
    void Start() {
        arduinoHelper = new ArduinoHelper();
    }

    void Update() {
        angleLeft = arduinoHelper.angle_l;
        angleRight = arduinoHelper.angle_r;

        angleLeftText.text = angleLeft.ToString();
        angleRightText.text = angleRight.ToString();

        NoteLeftConstraint();
        NoteRightConstraint();
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    void NoteLeftConstraint() {
        if (Input.GetKeyDown(KeyCode.O)) {
            angleLeftConstraint = angleLeft;
        }
    }

    void NoteRightConstraint() {
        if (Input.GetKeyDown(KeyCode.P)) {
            angleRightConstraint = angleRight;
        }
    }
}