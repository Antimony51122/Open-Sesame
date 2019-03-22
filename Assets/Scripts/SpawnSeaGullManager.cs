using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnSeaGullManager : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private Vector3    spawnPosSeaGull      = new Vector3(0f, 0f, 0f);
    [SerializeField] private float      speedSeaGull         = -12.5f;
    [SerializeField] private float      spawnSeaGullInterval = 8f;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    [SerializeField] private GameObject seaGullGameObject;
    private SeaGull seaGull;


    /////////////////
    /// Main Loop ///
    /////////////////

    void Start() {
        // trigger spawning new object, starting from 2s, with frequency of once each 2s
        InvokeRepeating("SpawnSeaGull", 2.0f, spawnSeaGullInterval);

        seaGull = seaGullGameObject.GetComponent<SeaGull>();
    }

    void Update() {
        float displacementSeaGull = Time.deltaTime * speedSeaGull;

        // store all children under Spawn SeaGull Manager in an array
        Transform[] seaGullChildren = transform.Cast<Transform>().ToArray();

        // this array exists to avoid error occuring at the beginning when the spawn seagull manager has no children entity
        if (seaGullChildren.Length > 0) {
            for (int i = 0; i < seaGullChildren.Length; i++) {
                var seaGullGhild = seaGullChildren[i];

                // check the whether hit by flush property of seagull child instance
                bool seaGullChildHitByFlush = seaGullGhild.GetComponent<SeaGull>().hitByFlush;


                // only fly towards left when the seagull has not been hit by the water flush
                if (!seaGullChildHitByFlush) {
                    seaGullGhild.transform.Translate(
                        Vector2.right * displacementSeaGull,
                        Space.World);
                }
            }
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    // sea gull is not part of the fish-trash system and the spawning rate is very low
    // thus doesn't need to be wrapped into the above object spawn-destroy system
    void SpawnSeaGull() {
        GameObject newSpawnSeaGull;

        newSpawnSeaGull = Instantiate(
            seaGullGameObject,
            spawnPosSeaGull,
            Quaternion.identity);
        newSpawnSeaGull.transform.parent = transform;
    }
}