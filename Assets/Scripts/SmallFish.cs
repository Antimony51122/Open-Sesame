using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFish : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private Sprite smallFishDefault;
    [SerializeField] private Sprite smallFishFrightened;
    [SerializeField] private Sprite smallFishLaugh;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    private PlayerHealth playerHealth;
    private Jaw jaw;

    /////////////////
    /// Main Loop ///
    /////////////////

    void Start() {
        playerHealth = FindObjectOfType<PlayerHealth>();
        jaw          = FindObjectOfType<Jaw>();
    }

    void Update() {
        ChangeSprites();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log(collision.gameObject.name);
        playerHealth.EatSmallFish();
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    // TODO: not show rage faces if the whale is in different altitude

    private void ChangeSprites() {
        //Debug.Log(transform.position.x);
        if (transform.position.x > -7f &&
            transform.position.x < 4f) {
            //Debug.Log(jaw.pivotCoordinate.x);
            // When the fish is close to the jaw but not being eaten yet
            GetComponent<SpriteRenderer>().sprite = smallFishFrightened;
        } else if (transform.position.x < -7f) {
            // When the fish passed the Whale, indicating the Whale missed capturing it
            GetComponent<SpriteRenderer>().sprite = smallFishLaugh;
        }
    }

}