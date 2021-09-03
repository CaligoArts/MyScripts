using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// My 1st CSharp script :) I will store all coding notes here.

public class HelloWorld : MonoBehaviour     // This must always match name of the script, If renaming script make sure to rename the public class also.
    // Creating a new script, creates a new public class that derives from the built in class called MonoBehaviour.
{
    // Declare variables at the top of the class to be useable by everything within the class. Variable holds a value that can vary.
    // Value types are typically int (integers), float (floating point numbers, i.e., numbers that can have decimals), string (text), & Boolean (true or false values).
    // Use camelCase for public variables so Unity Inspector will display them correctly as in "Camel Case" would display for camelCase.
     
    public string myMessage;    // This variable will display on whatever game object the script is set to & let you input what you want myMessage to say.
    // public makes the variable editable inside the editor, meaning you can set the value inside Unity rather than coding it in the script.

    // Vector3 is a data type that stores 3 values.
    public Vector3 scaleChange;     // This variable will store the x, y, z values for scale to change size of gameObject.

    public Vector3 positionChange;  // Increment position.

    public Vector3 rotateChange;    // Increment rotation.

    // Start is called before the first frame update. Runs once at game start.
    void Start()    // This is a function
    {
        // Debug.Log("Hello World");   // Displays a text string to the console window. Good to test if scripts working. Use under Update function to constantly check for condition.
        Debug.Log(myMessage);   // This will call the variable myMessage from above so it displays the message you set in the input in the Inspector window.
    }

    // Update is called once per frame. Basically constantly runs while game is running.
    void Update()   // This is a function
                    // There are roughly 24 frames per second so setting a transform under Update would be multiplying values 24 times every second.
    {
        transform.localScale += scaleChange;
        // transform.   Refers to Transform component of your GameObject.
        // localScale   Refers to Scale of the GameObject.
        // +=   Is an Operator that adds values set in scaleChange variable to the current scale values to make the ball grow.

        transform.position += positionChange;
        // position     Refers to position of GameObject. This will move GameObject from one place to another.

        transform.Rotate(rotateChange);     // Rotates GameObject based on variables set.
        // Rotate() is a method used to add rotation to the GameObject.
    }
}

// My 2nd Script written:
public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;      // Variable to allow setting vehicle speed in Inspector.
    private float turnSpeed = 50.0f;     // Inspector adjustable variable to turn vehicle.
    private float horizontalInput;   // Uses Edit> Project Settings> Input Manager> Axes to input for left, right movement.
    private float forwardInput;      // Allows assignment of forward, backward Player input.
    // Changed all variables above from public to private because once set there is no need to access them in Inspector & we don't want anything else changing them.

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

        horizontalInput = Input.GetAxis("Horizontal");  // Assigns horizontalInput variable to Player input on horizontal axis. (This was put at Top of Update() but didn't say why, works fine here.)
        forwardInput = Input.GetAxis("Vertical");   // Assigns forwardInput variable to Player input on vertical axis.
    }
}

// 3rd script written:
public class FollowPlayer : MonoBehaviour
{
    public GameObject player;   // Allows setting an object to be player in Inspector.
    private Vector3 offset = new Vector3(0.13f, 8, -7);     // Sets camera above & behind player position. (If using decimals must use f for float after number.)

    //Variables to move camera with player keeping view forward:
    private float horizontalInput;
    private float turnSpeed = 50.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // LateUpdate is called after Update() runs to make the camera run smoother & remove jittering caused by PlayerController & FollowPlayer both using Update().
    void LateUpdate()
    {
        // transform.position = player.transform.position;     // Assigns camera position to player position. Camera= Object script is attached to. Player= variable object that is assigned in Inspector.
        // transform.position = player.transform.position + new Vector3(0, 5, -7);     // Applies offset to camera so it sits above player for better view.
        transform.position = player.transform.position + offset;    // Clean code way to write above using private variable set above.

        // Rotate camera with vehicle: (Only if 1st person view, doesn't work in 3rd person with offset.)
        horizontalInput = Input.GetAxis("Horizontal");
        // transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);     // This would work on 1st person but as its on 3rd person it rotates on offset so no good.
    }
}

