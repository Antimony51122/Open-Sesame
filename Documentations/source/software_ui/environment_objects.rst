.. figure:: ../_static/Software_UI/Environment/Cover.jpg
    :align: center

Environment Objects
===================

Scene Object Leftwards Movement
-------------------------------

In order to convey the effect that whale is swimming towards right whilst its relative x-position to the screen boundary maintains, functions need to be defined to let the various objects such as corel, wave and clouds scroll to the left at different speeds which also engaged a parallel effect between further and closer objects.

.. code-block:: C#

    [SerializeField] private float scrollSpeed = -4f;
    [SerializeField] private int resetX = -32;

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

        ...
    }

.. figure:: ../_static/Software_UI/Environment/WaveEntity_Editor.jpg
    :align: center
    :figclass: align-center

    ``WaveEntity`` Object Inspector Screenshot

.. note:: In order to create a constant flow movement of the wave, 3 identical wave sprites with the same width as the screen width have been put in a row. When the center of the ``WaveEntity`` which is the container of 3 wave sprites scrolls to one screen width to the left of the original center, reset the X position of the Wave entity to it's original starting position thus create a constance flowing effect.

.. tip:: An oscillating algorithm has been implemented on the wave entity to mimic the dynamic of real waves. 3 layers of wave entities fluctuate according to various periods

.. code-block:: C#

    // for sine periodic oscillation movement implementation
    [SerializeField] private bool isOscillating = false;
    [SerializeField] private Vector2 movementVector = new Vector2(0f, 0.25f);
    [SerializeField] float period = 2f;

    // 0 for not moved, 1 for fully moved.
    [Range(0, 1)] [SerializeField] private float movementFactor;

    private Vector2 offset;

    ...

    void Update() {
        ...

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