using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private float scrollSpeed = -4f;
    [SerializeField] private int   resetX      = -32;

    private Vector3 startPos = new Vector3();
    private float xPos;
    private float yPos;

    // for sine periodic oscillation movement implementation
    [SerializeField] private bool isOscillating = false;
    [SerializeField] private Vector2 movementVector = new Vector2(0f, 0.25f);
    [SerializeField] float period = 2f;

    // 0 for not moved, 1 for fully moved.
    [Range(0, 1)] [SerializeField] private float movementFactor;

    private Vector2 offset;


    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        // override the start position to its initial sprite position
        startPos = transform.position;
    }

    void Update() {
        xPos = transform.position.x;
        yPos = transform.position.y;

        float displacement = Time.deltaTime * scrollSpeed;
        transform.Translate(Vector2.right * displacement);

        // when the center of Wave scrolls to one screen width to the left of the original center,
        // reset the X of the Wave entity to it's original starting position
        if (xPos < resetX) {
            transform.position = new Vector3(startPos.x, yPos, startPos.z);
        }

        // ------- oscillation movement implementation -------
        // protect against period is zero
        // period less than or equal to the smallest thing we can represent (as good as 0)
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // grows continually from 0

        const float tau = Mathf.PI * 2f; // about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

        movementFactor = rawSinWave / 2f;
        offset = movementFactor * movementVector;

        if (isOscillating) {
            transform.Translate(Vector2.down * offset);
        }
    }

}