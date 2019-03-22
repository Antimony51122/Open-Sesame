using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaGull : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------


    // determine whether the seagull has been hit by water flush or not
    // set public to be accessed by the seagull manager class to deal with collision issues
    public bool hitByFlush;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    [SerializeField] private GameObject seaGullManagerGameObject;
    private SpawnSeaGullManager spawnSeaGullManager;

    private Rigidbody2D rigidbody2D;
    private Sprite currentSprite;
    private Animator animator;

    void Start() {
        //
        rigidbody2D = GetComponent<Rigidbody2D>();
        //
        currentSprite = GetComponent<SpriteRenderer>().sprite;
        //
        animator = GetComponent<Animator>();

        // create reference to the 
        spawnSeaGullManager = seaGullManagerGameObject.GetComponent<SpawnSeaGullManager>();

        // initialise the seagull with state not being hit by the flush
        hitByFlush = false;
    }

    void Update() {
        DestroySeaGullHierarchy();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "Splash") {
            HitByFlush();
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    private void HitByFlush() {
        // set the property of hitByFlush to true to stop the entity from moving towards left
        hitByFlush = true;

        ChangeAnimation();
        ChangeRigidBodyType();
    }

    private void ChangeAnimation() {
        animator.SetBool("IsHitByFlush", true);
    }

    private void ChangeRigidBodyType() {
        // change to rigidbody type to dynamics thus could use gravity
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    // ------- Destroy the instance when it's outside screen to save memory -------

    public void DestroySeaGullHierarchy() {
        //Debug.Log(gameObject.transform.position.x);
        if (gameObject.transform.position.x < -18f) {
            Destroy(gameObject);
        }
    }
}