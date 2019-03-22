using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashManager : MonoBehaviour {
    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    [SerializeField] private GameObject whaleGameObject;
    private Whale whale;

    private BoxCollider2D  box2D;
    private Animator       animator;
    private SpriteRenderer spriteRenderer;


    void Start() {
        whale = whaleGameObject.GetComponent<Whale>();

        // initially disable the box collider, animator and sprite render and trigger later 
        box2D         = GetComponent<BoxCollider2D>();
        box2D.enabled = false;

        animator         = GetComponent<Animator>();
        animator.enabled = false;

        spriteRenderer         = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    void Update() {
        Debug.Log(whale.isMovingDownValid); // determine whether the whale is at higher position

        if (whale.isActiveAndEnabled) {
            KeyboardControlSplash();
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    // ------- Keyboard Control -------

    void KeyboardControlSplash() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ActivateSplash();
        }
    }

    // ------- Button Control -------

    void ButtonControlSplash() {

    }



    void ActivateSplash() {
        box2D.enabled = true;
        animator.enabled = true;
        spriteRenderer.enabled = true;
        Invoke("DeactivateSplash", 0.3f);
    }

    void DeactivateSplash() {
        box2D.enabled = false;
        animator.enabled = false;
        spriteRenderer.enabled = false;
    }
}