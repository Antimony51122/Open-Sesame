using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationMenu : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private float angleLeft;
    [SerializeField] private float angleRight;

    // the 2 angle constraints will be accessed by the Jaw class
    public float angleLeftConstraint  = 30f;
    public float angleRightConstraint = 50f;

    [SerializeField] private TextMeshProUGUI angleLeftText;
    [SerializeField] private TextMeshProUGUI angleRightText;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    [SerializeField] private GameObject arduinoGameObjectCalibration;
    private ArduinoHelper arduinoHelper;

    private WriteTextHelper writeTextHelper;

    /////////////////
    /// Main Loop ///
    /////////////////

    void Awake() {
        arduinoHelper = arduinoGameObjectCalibration.GetComponent<ArduinoHelper>();

        writeTextHelper = new WriteTextHelper();
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

        KeyboardNoteConstraints();

        writeTextHelper.WriteString(angleLeft.ToString(), angleRight.ToString());
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    // ------- Keyboard Control -------
    private void KeyboardNoteConstraints() {
        if (Input.GetKeyDown(KeyCode.O)) {
            NoteLeftConstraint();
        } else if (Input.GetKeyDown(KeyCode.P)) {
            NoteRightConstraint();
        }
    }

    public void NoteLeftConstraint() {
        angleLeftConstraint = angleLeft;
    }

    public void NoteRightConstraint() {
        angleRightConstraint = angleRight;
    }
}