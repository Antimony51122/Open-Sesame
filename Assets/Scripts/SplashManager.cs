using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashManager : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private float splashDuration = 0.5f;
    private int buttonPressed = 0;

    private bool isSplashActivatable;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    // for Arduino Button Input Handling
    [SerializeField] private GameObject arduinoGameObject;
    private ArduinoHelper arduinoHelper;

    [SerializeField] private GameObject whaleGameObject;
    private Whale whale;

    private BoxCollider2D  box2D;
    private Animator       animator;
    private SpriteRenderer spriteRenderer;


    /////////////////
    /// Main Loop ///
    /////////////////

    void Start() {
        arduinoHelper = arduinoGameObject.GetComponent<ArduinoHelper>();
        whale         = whaleGameObject.GetComponent<Whale>();

        // initially disable the box collider, animator and sprite render and trigger later 
        box2D         = GetComponent<BoxCollider2D>();
        box2D.enabled = false;

        animator         = GetComponent<Animator>();
        animator.enabled = false;

        spriteRenderer         = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        // initially set the splash activatable to true
        isSplashActivatable = true;
    }

    void Update() {
        buttonPressed = arduinoHelper.buttonNum;

        // determine whether the whale altitude and only trigger at higher position
        if (whale.isMovingDownValid && isSplashActivatable) {
            KeyboardControlSplash();
            ButtonControlSplash();
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    // ------- Keyboard Control -------

    void KeyboardControlSplash() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ActivateSplash();

            PreventMultipleSplash();
        }
    }

    // ------- Button Control -------

    void ButtonControlSplash() {
        if (buttonPressed == 1) {
            ActivateSplash();

            PreventMultipleSplash();
        }
    }

    // ------- Enable and Disable Splash Activatable to mitigate splash overlay -------

    void PreventMultipleSplash() {
        // prevent the user from splashing various times within short time
        isSplashActivatable = false;

        // set the splash activatable property back to true after a short delay
        Invoke("SplashActivatable", 0.5f);
    }

    void SplashActivatable() {
        isSplashActivatable = true;
    }

    // ------- Splash Manipulations -------

    void ActivateSplash() {
        box2D.enabled          = true;
        animator.enabled       = true;
        spriteRenderer.enabled = true;
        Invoke("DeactivateSplash", splashDuration);
    }

    void DeactivateSplash() {
        box2D.enabled          = false;
        animator.enabled       = false;
        spriteRenderer.enabled = false;
    }
}