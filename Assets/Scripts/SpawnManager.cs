using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class SpawnManager : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private bool isBothLegMode;

    [SerializeField] private GameObject smallFish;
    [SerializeField] private GameObject bigFish;
    [SerializeField] private GameObject trash;

    [SerializeField] private float   spawnInterval  = 2f;
    [SerializeField] private Vector3 spawnPosHigher = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 spawnPosLower  = new Vector3(0f, 0f, 0f);
    [SerializeField] private float   speed          = -10f;

    // initialise an unassigned final spawn position to select from one of the higher or lower
    private Vector3 spawnPos;


    /////////////////
    /// Main Loop ///
    /////////////////

    void Start() {
        // trigger spawning new object, starting from 2s, with frequency of once each 2s
        InvokeRepeating("spawnObject", 2.0f, spawnInterval);
    }

    void Update() {
        float displacement = Time.deltaTime * speed;

        // store all children under Spawn Manager in an array
        Transform[] children = transform.Cast<Transform>().ToArray();

        for (int i = 0; i < children.Length; i++) {
            var child = children[i];
            // beware to add Space.World or otherwise default will be Space.Self
            // where rotation angle of the object will be stored as well
            child.transform.Translate(Vector2.right * displacement, Space.World);
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    // TODO: trash coming on both altitude, or otherwise the player doesn't need to close mouth anymore
    // TODO: gradually increase the speed of object moving towards left
    // TODO: Choose between left, right both legs + seagull mode

    private void spawnObject() {
        // instantiate the next spawn
        GameObject newSpawn;

        // random 1/3 possibility spawning each of the 3 plausible objects
        // random 1/2 possibility spawning at each of the 2 plausible altitude
        Random random = new Random();
        int randomThresholdObject = random.Next(1, 4); // generate a integer number between 1, 2, 3
        int randomThresholdPos    = random.Next(1, 3); // generate a integer number between 1, 2

        // determine the altitude of the object spawn position, assigning values to spawnPos
        if (isBothLegMode) {
            if (randomThresholdPos == 1) {
                spawnPos = spawnPosHigher;
            } else if (randomThresholdPos == 2) {
                spawnPos = spawnPosLower;
            }
        } else {
            spawnPos = spawnPosHigher;
        }
        

        // determine which object will be spawned at the previous defined altitude
        if (randomThresholdObject == 1) {
            newSpawn = Instantiate(
                smallFish,
                spawnPos,
                Quaternion.identity);
            addChildToCurrentObject(newSpawn);
        } else if (randomThresholdObject == 2) {
            newSpawn = Instantiate(
                bigFish,
                spawnPos,
                Quaternion.identity);
            addChildToCurrentObject(newSpawn);
        } else if (randomThresholdObject == 3) {
            newSpawn = Instantiate(
                trash,
                spawnPos,
                Quaternion.Euler(0, 0, -20f)); // beware the trash spawn has rotation angle
            addChildToCurrentObject(newSpawn);
        }
    }

    void addChildToCurrentObject(GameObject item) {
        // make the current item a child of the SpawnManager
        item.transform.parent = transform;
    }
    

}