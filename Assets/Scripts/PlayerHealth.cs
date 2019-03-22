using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    private int score;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private float health             = 1f;
    [SerializeField] private float healthMax          = 1f;
    [SerializeField] private float healthMin          = 0f;
    [SerializeField] private float healthWarn         = 0.3f;
    [SerializeField] private float healthDecreaseRate = 0.001f;

    private Transform barMask;
    private Transform bar;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    [SerializeField] private GameObject sceneLoaderGameObject;
    private SceneLoader sceneLoader;


    /////////////////
    /// Main Loop ///
    /////////////////

    void Awake() {
        barMask = transform.Find("Green Bar Mask");
        bar     = transform.Find("Green Bar");

        sceneLoader = sceneLoaderGameObject.GetComponent<SceneLoader>();
    }

    void Start() {
        // initialise the player score with 0
        score = 0;

        scoreText.text = score.ToString();
    }

    void Update() {
        scoreText.text = score.ToString();

        ConstantHealthDecrease();
        SetSize(health);

        // under 30% health, start warning
        if (health > healthMax) {
            health = healthMax;
        } else if (health <= healthMin) {
            // player dead, load game over scene to reload
            sceneLoader.LoadReloadScene();
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    // ----- Bar Appearance Manipulation -----

    private void SetSize(float sizeNormalised) {
        barMask.localScale = new Vector3(sizeNormalised, 1f);
    }

    private void SetColour(Color colour) {
        bar.GetComponent<SpriteRenderer>().color = colour;
    }

    // ----- Health Manipulations -----

    private void ConstantHealthDecrease() {
        if (health > healthMin) {
            health -= healthDecreaseRate;
        }
    }

    // ----- Eaten Behaviour -----

    public void EatSmallFish() {
        health += 0.2f;
        score += 20;
    }

    public void EatBigFish() {
        health += 0.4f;
        score += 40;
    }

    public void EatTrash() {
        health -= 0.6f;
    }

    // ----- Splash SeaGull -----

    public void SplashSeaGull() {
        score += 60;
    }
}
