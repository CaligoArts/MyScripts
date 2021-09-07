using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerController : MonoBehaviour
{
    private float speed = 15.0f;      // Variable to allow setting vehicle speed in Inspector.
    private float turnSpeed = 50.0f;     // Inspector adjustable variable to turn vehicle.
    private float horizontalInput;   // Uses Edit> Project Settings> Input Manager> Axes to input for left, right movement.
    private float forwardInput;      // Allows assignment of forward, backward Player input.
    // Changed all variables above from public to private because once set there is no need to access them in Inspector & we don't want anything else changing them.

    // 3rd Person view to 1st person view:
    public Camera mainCamera;
    public Camera fpvCamera;
    public KeyCode switchKey;

    public string inputID;  //Determines which player is using the script.

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(0, 0, 1);    // Move the vehicle forward.
        // Translate    Moves the transform in the direction & distance of translatation (x, y, z)
        // transform.Translate(Vector3.forward);   // Cleaner way to write the above code.
        // forward is a preconfigured Vector3 for 0, 0, 1.
        // transform.Translate(Vector3.forward * Time.deltaTime * 20);     // Moves forward 20 meters every second.
        // Time.deltaTime   Gets the change in time between each frame to change update from every frame to every second.
        // transform.Translate(Vector3.forward * Time.deltaTime * speed);  // Replaced hardcoded 20 with variable speed for clean code.
        // transform.Translate(Vector3.right * Time.deltaTime * turnSpeed);    // Uses turnSpeed variable to turn right.
        // transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);  // Uses Player input to move right or left using variable. (Slides doesn't rotate)
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);   // Uses Player input to move forward or backward so now only moves when Player hits key.
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);     // Rotates the vehicle for more realism so not just sliding side to side.

        //horizontalInput = Input.GetAxis("Horizontal");  // Assigns horizontalInput variable to Player input on horizontal axis. (This was put at Top of Update() but didn't say why, works fine here.)
        //forwardInput = Input.GetAxis("Vertical");   // Assigns forwardInput variable to Player input on vertical axis.

        //Two Player controls: (Takes into account which inputID is used. Inputs must be setup in Unity Edit> Project settings as Horizontal1/ Horizontal2 & Vertical1/ Vertical2)
        horizontalInput = Input.GetAxis("Horizontal" + inputID);
        forwardInput = Input.GetAxis("Vertical" + inputID);

        // 3rd Person view to 1st person view:
        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            fpvCamera.enabled = !fpvCamera.enabled;
        }
    }
}
