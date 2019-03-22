using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaGull : MonoBehaviour
{
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private Sprite seaGullChildShocked;

    // determine whether the seagull has been hit by water flush or not
    // set public to be accessed by the seagull manager class to deal with collision issues
    public bool hitByFlush;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    [SerializeField] private GameObject seaGullManagerGameObject;
    private SpawnSeaGullManager spawnSeaGullManager;

    void Start()
    {
        // create reference to the 
        spawnSeaGullManager = seaGullManagerGameObject.GetComponent<SpawnSeaGullManager>();

        // initialise the seagull with state not being hit by the flush
        hitByFlush = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    private void ChangeSprites() {
        // set the property of hitByFlush to true to stop the entity from moving towards left
        hitByFlush = true;



    }
}
