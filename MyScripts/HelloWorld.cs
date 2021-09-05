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
        // transform.Translate(Vector3.back * speed);   // Moves backwards times speed.
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

// Camera Control Scripts not covered in tutorial. Info found at https://code.tutsplus.com/tutorials/unity3d-third-person-cameras--mobile-11230

// Look at camera: ( Camera doesn't move location but follows player like a turrent.)

public class LookAtCamera : MonoBehaviour
{
    public GameObject target;   // This needs to be set in Inspector to tell the camera what to focus on.

    void LateUpdate()
    {
        transform.LookAt(target.transform);     // Tells the camera to follow targets location.
    }
}

// Dungeon crawler camera: ( Sits above player & moves relative to player but never rotates.)

public class DungeonCamera : MonoBehaviour
{
    public GameObject target;   // This needs to be set in Inspector to tell the camera what to focus on.
    Vector3 offset;
    public float damping = 1;   // Dampen the movement slightly so that it takes some time to catch up to the player.

    void Start()
    {
        offset = transform.position - target.transform.position;    // Calculates offset when script is first run.
    }

    void LateUpdate()
    {
        // Update the camera's position based on the player's position by applying the offset.
        Vector3 desiredPosition = target.transform.position + offset;
        transform.position = desiredPosition;

        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);     // Dampen position between 2 points: current position and desired position.
        // Vector3.Lerp() method - Lerp linearly interpolates between two points, meaning it smoothly transitions from one point to another in a straight line.

        transform.LookAt(target.transform.position);    // Tells the camera to follow targets location.
    }
}

// The Follow Camera: ( Sits above & behind player & rotates around player as they turn.)

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    public float damping = 1;
    Vector3 offset;

    void Start()
    {
        offset = target.transform.position - transform.position;    // Only calculates offset when script is 1st run so it doesn't effect rotation.
    }

    void LateUpdate()
    {
        float currentAngle = transform.eulerAngles.y;   // Gets current angle on y axis.
        float desiredAngle = target.transform.eulerAngles.y;    // Gets the angle of the target.
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, damping);    // Lerps between angle of camera & angle of player & applies damping.
        // Mathf.LerpAngle() - Method used to lerp between angles instead of position.

        Quaternion rotation = Quaternion.Euler(0, angle, 0);    // Turns the angle of target into rotation.
        transform.position = target.transform.position - (rotation * offset);   // Multiplies offset by rotation to orient offset same as target & subtract the result from target position.

        transform.LookAt(target.transform);     // Keep looking at the player.
    }
}

// The Mouse Aim Camera: (Similar to Follow camera but rotation is controlled my mouse movement which points player in direction the camera is facing.)

public class MouseAimCamera : MonoBehaviour
{
    public GameObject target;
    public float rotateSpeed = 5;   // Rotation speed of camera as mouse moves.
    Vector3 offset;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;  // Accesses horizontal axis of mouse.
        target.transform.Rotate(0, horizontal, 0);  // Uses horizontal axis of mouse to rotate player. 

        // Orient offset in same direction & subtract from targets position to keep the camera behind the player:
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(target.transform);
    }
}