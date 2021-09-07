using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;  //Gives ability to switch between scenes.
using UnityEngine.UI;   //Access UI elements.

//If using Editor then it applies the code to quit testing otherwise is applies code to exit built game:
#if UNITY_EDITOR
using UnityEditor;
#endif

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

// My 2nd Script written: (Written for Unity Junior Programmer Pathway Prototype1)
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

    //Variables to move camera with player keeping view forward: (Attempted but not correct way to accomplish)
    //private float horizontalInput;
    //private float turnSpeed = 50.0f;

    // LateUpdate is called after Update() runs to make the camera run smoother & remove jittering caused by PlayerController & FollowPlayer both using Update().
    void LateUpdate()
    {
        // transform.position = player.transform.position;     // Assigns camera position to player position. Camera= Object script is attached to. Player= variable object that is assigned in Inspector.
        // transform.position = player.transform.position + new Vector3(0, 5, -7);     // Applies offset to camera so it sits above player for better view.
        transform.position = player.transform.position + offset;    // Clean code way to write above using private variable set above.

        // Rotate camera with vehicle: (Only if 1st person view, doesn't work in 3rd person with offset.)
        //horizontalInput = Input.GetAxis("Horizontal");
        // transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);     // This would work on 1st person but as its on 3rd person it rotates on offset so no good.
    }
}

//Written for Prototype1 Bonus Challenges:

public class OncomingVehicles : MonoBehaviour
{
    private float speed = 5.0f; //Controls speed of oncoming vehicles.

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);  //Moves object script is attached to forward x speed x ever second.
    }
}

//Updated PlayerController to include switching camera from 3rd person view to 1st person view:
public class PlayerController : MonoBehaviour
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

        // 3rd Person view to 1st person view:
        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            fpvCamera.enabled = !fpvCamera.enabled;
        }
    }
}

//First Person View Camera controller:

public class FPVFollowPlayer : MonoBehaviour
{
    public GameObject player;   // GameObject for camera to follow.
    private Vector3 offset = new Vector3(0, 4.3f, 0);   //Offset to position camera above player object.

    private float horizontalInput;  //Variable to store left, right input from player.
    private float turnSpeed = 50.0f;    //Speed to turn as player presses input.

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;    //Starting position of camera.
        //horizontalInput = Input.GetAxis("Horizontal");  //Uses the Horizontal axis set in Unity to control left, right movement. (Don't need this since it's controlled in PlayerController.cs if its a child of Player GameObject.)
        transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);    //Rotates camera based on left, right player input times turnSpeed times once a second.
    }
}

// Need to find better way to control camera for 3rd person view.

//Replaced FollowPlayer script with FollowCamera script listed below for better 3rd person view control.

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
// Camera used in Unity Prototype1 Bonus challenges:

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    public float damping = 1;
    Vector3 offset;

    void Start()
    {
        //Original code from notes. Doesn't accomplish what I'm trying to do (positioning is off):
        //offset = player.transform.position - transform.position;    // Calculates offset when script is 1st run based on targets position - position of object script is attached to.

        //Assigned offset to attempt more control over camera position but doesn't seem to set exactly how intended:
        offset = new Vector3(0, -11, 1); //Working almost how I want but camera is too far back & not sure why values change as they do.
    }

    void LateUpdate()
    {
        float currentAngle = transform.eulerAngles.y;   // Gets current angle on y axis.
        float desiredAngle = player.transform.eulerAngles.y;    // Gets the angle of the target.
        //float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);
        //Had to remove Time.deltaTime because on Play the camera just continously rotated around target.
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, damping);    // Lerps between angle of camera & angle of player & applies damping.
        // Mathf.LerpAngle() - Method used to lerp between angles instead of position.

        //Original notes but positioning off:
        //Quaternion rotation = Quaternion.Euler(0, angle, 0);    // Turns the angle of target into rotation.
        Quaternion rotation = Quaternion.Euler(-55, angle, -5); //Working. Had to set x & z axis to correct camera angle.
        transform.position = player.transform.position - (rotation * offset);   // Multiplies offset by rotation to orient offset same as target & subtract the result from target position.

        transform.LookAt(player.transform);     // Keep camera looking at the player.
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

//Copied PlayerController.cs & added TwoPlayer controls.
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

public class GameManager : MonoBehaviour
//Create an Empty GameObject to attach script to so you can target it in the OnClick function of the Button.
//** Must include namespaces at top:
//using UnityEngine.SceneManagement;  //Gives ability to switch between scenes.
//using UnityEngine.UI;   //Access UI elements.

//If using Editor then it applies the code to quit testing otherwise is applies code to exit built game:
//#if UNITY_EDITOR
//using UnityEditor;
//#endif

{
    //These are Methods for Buttons to access to give them scene control:
    public void OnePlayer()
    {
        SceneManager.LoadScene(1);
    }

    public void TwoPlayer()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();   //Exits Playmode when using Editor.
#else
        Application.Quit();     //Exits application after built.
#endif
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}