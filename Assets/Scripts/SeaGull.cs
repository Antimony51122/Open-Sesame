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
    public bool fallIntoWater;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    private Rigidbody2D rigidbody2D;
    private Animator    animator;

    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator    = GetComponent<Animator>();

        // initialise the seagull with state not being hit by the flush, nor fallen into water
        hitByFlush    = false;
        fallIntoWater = false;
    }

    void Update() {
        FlowWithWater();
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
        StartCoroutine(ChangeZIndex());
    }

    // make the z-index further thus it can be hidden in water when fallen
    private IEnumerator ChangeZIndex() {
        yield return new WaitForSeconds(0.50f);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);
    }

    private void ChangeAnimation() {
        animator.SetBool("IsHitByFlush", true);
    }

    private void ChangeRigidBodyType() {
        // change to rigidbody type to dynamics thus could use gravity
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    // get rid off all downwards force and make the object slowly move with water towards left
    private void FlowWithWater() {
        if (transform.position.y < 0.5f) {
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = new Vector3(-1.5f, 0, 0);
        }
    }

    // ------- Destroy the instance when it's outside screen to save memory -------

    public void DestroySeaGullHierarchy() {
        //Debug.Log(gameObject.transform.position.x);
        if (gameObject.transform.position.x < -18f) {
            Destroy(gameObject);
        }
    }
}