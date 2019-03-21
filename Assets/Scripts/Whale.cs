using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    // determine whether the moving down posture of the whale is valid
    // when at higher position, moving down is valid while moving up is invalid and vice versa
    private bool isMovingDownValid;

    // define the 3 states the whale's  postures switches between 
    enum State {
        movingDown,
        movingUp,
        stop
    }

    // define the moving up and down speed
    private float speed;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    private State state;
    private State previousState;

    private Jaw jaw;

    /////////////////
    /// Main Loop ///
    /////////////////

    void Start() {
        // start with higher position where moving down is valid while moving up is invalid
        isMovingDownValid = true;

        // initialise the state with stop state where the whale keeps the position
        state         = new State();
        state         = State.stop;
        previousState = new State();
        previousState = State.stop;

        // initialise the whale position translation speed
        speed = 5.0f;
    }

    void Update() {
        KeyboardControl();
        MovementHandler();

        //PrintState();
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    private void PrintState() {
        if (state != previousState) {
            Debug.Log(state);
            state = previousState;
        }
    }

    private void MovementHandler() {
        switch (state) {
            case State.movingDown:
                transform.Translate(
                    -Vector3.up * speed * Time.deltaTime,
                    Space.World);
                break;
            case State.movingUp:
                transform.Translate(
                    Vector3.up * speed * Time.deltaTime,
                    Space.World);
                break;
            case State.stop:
                // stop the whale by assign the current position to its position
                transform.position = gameObject.transform.position;
                break;
            default:
                transform.position = gameObject.transform.position;
                break;
        }
    }

    // ----- Keyboard Control -----

    void KeyboardControl() {
        if (Input.GetKey(KeyCode.S)) {
            StartCoroutine(MoveDown());
        } else if (Input.GetKey(KeyCode.W)) {
            StartCoroutine(MoveUp());
        }
    }

    // ----- Potentiometer Control -----

    void PotentiometerControl() {
    }

    // ----- Change Movements by Manipulating States -----

    private IEnumerator MoveDown() {
        if (isMovingDownValid) {
            state = State.movingDown;
            yield return new WaitForSeconds(0.75f); // give 0.75s position translation time
            state = State.stop;

            // banning the whale from moving further downwards when it's already in lower position
            isMovingDownValid = false;
        }
    }

    private IEnumerator MoveUp() {
        if (!isMovingDownValid) {
            state = State.movingUp;
            yield return new WaitForSeconds(0.75f);
            state = State.stop;

            // banning the whale from moving further upwards when it's already in higher position
            isMovingDownValid = true;
        }
    }

}