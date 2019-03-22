using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationMenu : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private float angleLeft;
    [SerializeField] private float angleRight;

    // the 2 angle constraints will be accessed by the Jaw class
    public float angleLeftConstraint;
    public float angleRightConstraint;

    [SerializeField] private Text angleLeftText;
    [SerializeField] private Text angleRightText;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    [SerializeField] private GameObject arduinoGameObjectCalibration;
    private ArduinoHelper arduinoHelper;


    /////////////////
    /// Main Loop ///
    /////////////////

    void Awake() {
        arduinoHelper = arduinoGameObjectCalibration.GetComponent<ArduinoHelper>();
    }

    void Start() {
        angleLeft = 0;
        angleRight = 0;
    }

    void Update() {
        angleLeft = arduinoHelper.angle_l;
        angleRight = arduinoHelper.angle_r;

        angleLeftText.text = angleLeft.ToString();
        angleRightText.text = angleRight.ToString();

        // set the angle constraints of two legs
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