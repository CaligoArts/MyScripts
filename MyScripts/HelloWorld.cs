using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;  //namespace Gives ability to switch between scenes.
using UnityEngine.UI;   //namespace to Access UI elements. Like Sliders.

using TMPro;    //Allows you to use TextMeshProUGUI to change text on screen.

//If using Editor then it applies the code to quit testing otherwise is applies code to exit built game:
#if UNITY_EDITOR
using UnityEditor;
#endif

// My 1st CSharp script :) I will store all coding notes here.

//The 1st & Most Important thing to learn is that typing // before anything will comment it out telling the machine to ignore it so you can leave yourself or others notes explaining what the code does.

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
    void Start()    // This is a function (Actually I believe this is called a Method but not 100%)
    {
        // Debug.Log("Hello World");   // Displays a text string to the console window. Good to test if scripts working. Use under Update function to constantly check for condition.
        Debug.Log(myMessage);   // This will call the variable myMessage from above so it displays the message you set in the input in the Inspector window.
    }

    // Update is called once per frame. Basically constantly runs while game is running.
    void Update()   // This is a function (Actually I believe this is called a Method but not 100%)
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
    //PlayerController for a vehicle type driving experience.

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

//***Scripts written for Unity Prototype2:

public class PlayerControllerP2 : MonoBehaviour
{
    //Player controller for a top down move left, right, forward, backward with a stationary camera & movement boundaries that allows shooting projectiles forward.

    public float horizontalInput;   //Variable to track user input left/ right.
    public float verticalInput;     //Variable to track user input up/ down.
    public float speed = 15.0f;     //Controls speed that the input moves left or right.
    public float xRange = 15;       //Range of movement on the x axis to replace hardcoded values.

    public float zTopBoundry = 15;     //Forward boundry.
    public float zBottomBoundry = 0;   //Back boundry.

    public GameObject projectilePrefab;     //Allows setting of a GameObject to be a projectile in the Inspector for the player.

    public Transform projectileSpawnPoint;      //Spawn point for projectile so it doesn't interfere with the player collider when launched.
    //*Make sure to create a child game object on the player and position it infront of player to assign as the projectile spawn position.

    // Update is called once per frame
    void Update()
    {
        // Left/ Right movement:
        horizontalInput = Input.GetAxis("Horizontal");  //Assigns variable to Input manager horizontal Axis in Unity Editor.
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);  //Moves player left/ right based on user input times 1 second times speed set in variable.

        // Up/ Down movement:
        verticalInput = Input.GetAxis("Vertical");  //Assigns variable to Input manager vertical Axis in Unity Editor.
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);     //Moves player up/ down based on user input times 1 second times speed set in variable.

        //Create Boundry for the player movement left to right:
        //if (transform.position.x < -10)     //Checks if players x position is less than -10 (to the left side of screen)
        if (transform.position.x < -xRange)     //Removed & replaced hardcoded value with variable.
        {
            //transform.position = new Vector3(-10, transform.position.y, transform.position.z);  //Sets player position  to its current position with a fixed x location so can't go past -10 on the x axis.
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);  //Removed & replaced hardcoded value with variable.
        }

        //if (transform.position.x > 10)      //Checks if player x position is greater than 10 (to right side of screen)
        if (transform.position.x > xRange)  //Removed & replaced hardcoded value with variable.
        {
            //transform.position = new Vector3(10, transform.position.y, transform.position.z);   //Stops player if goes past 10 on x axis.
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);   //Removed & replaced hardcoded value with variable.
        }

        //Top / Bottom Boundry:
        if (transform.position.z > zTopBoundry)    //Checks if moves forward past top boundry
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zTopBoundry);     //Stops it if past top boundry.
        }

        if (transform.position.z < zBottomBoundry)     //Checks if moves back past bottom boundary.
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBottomBoundry);  //Stops it if past bottom boundary.
        }

        //Launch a projectile when spacebar is pressed:
        if (Input.GetKeyDown(KeyCode.Space))    //Checks if Spacebar has been pressed.
        {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectilePrefab.transform.rotation);     //Launches copy of projectilePrefab from players current position based on prefabs rotation.
            //Instantiate() method creates a copy of a GameObject.
        }
    }
}

public class PlayerControllerX : MonoBehaviour
//Controls how often a projectile can be launched: (Basically a FireRate CoolDown Controller.)
{
    public GameObject dogPrefab;

    //Fire Rate Variables:
    private float fireRate = 1; //Time the player has to wait to fire again
    private float nextFire = 0;     //Time since start after which player can fire again.

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, if enough time has elapsed since last fire, send dog
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)    //Checks if spacebar pressed and if game time passed is greater than the nextFire variable.
                                                                        //Time.time is the time in seconds since the game started.
        {
            nextFire = Time.time + fireRate;    //Resets nextFire to current time + fireRate
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);   //Creates a copy of dogPrefab & sends it on screen based on it's rotation.
        }
    }
}

public class ProjectileForward : MonoBehaviour
{
    //Shoots GameObject script is attached to forward.

    public float speed = 40.0f;     //Speed projectile moves.

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);  //Moves projectile forward times 1sec. times speed variable.
    }
}

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 30;    //Sets top boundary at 30.
    private float lowerBound = -10;     //Sets bottom boundry at -10.

    private float sideBound = 30;   //Sets side boundary at 30.

    private GameManager gameManager;    //Creates a reference to GameManager script. 

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   //Links to the GameManager script so you can use its methods inside this class script.
    }

    // Update is called once per frame
    void Update()
    {
        //If object goes past play area than remove that object:
        if (transform.position.z > topBound)    //Checks if position on z axis is greater than top boundry variable.
        {
            Destroy(gameObject);    //Destroys game object if goes past top boundry.
        }
        else if (transform.position.z < lowerBound)     //Check if position on z axis is less than bottom boundry variable.
        {
            gameManager.AddLives(-1);   //Tells the GameManager.cs to -1 life.
            Destroy(gameObject);    //Destroys game object if goes past bottom boundry.
            //Debug.Log("Game Over!");    //Displays Game Over! in console if object goes beyond boundries set.     //Removing to call GameManager script instead.
        }
        else if (transform.position.x > sideBound)
        {
            //Debug.Log("Game Over!");    //Removing to call GameManager script instead.
            gameManager.AddLives(-1);   //Tells the GameManager.cs to -1 life.
            Destroy(gameObject);
        }
        else if (transform.position.x < -sideBound)
        {
            gameManager.AddLives(-1);   //Tells the GameManager.cs to -1 life.
            //Debug.Log("Game Over!");      //Removing to call GameManager script instead.
            Destroy(gameObject);
        }
    }
}

public class SpawnManager : MonoBehaviour
{
    //*Create an Empty GameObject in Editor to attach SpawnManager script to with the same name.

    //Define which animal prefab spawns:
    public GameObject[] animalPrefabs;  //Allows assigning of an Array of GameObjects in Inspector.
    //public int animalIndex;     //Interger variable to use as a number value for which prefab is spawned. 0 spawns first prefab game object in array, 1 spawns 2nd in array, 2 spawns 3rd in array, & so on.
    //Removed Global variable because it's only needed locally in the if() statement below. Meaning nothing else needs to make use of the variable anywhere else in the script.

    //Define location of prefab spawns:
    //Top Spawn Location:
    private float spawnRangeX = 15;     //Sets spawn range on x axis to 20.
    private float spawnPosZ = 20;       //Sets position of where to spawn on z axis to 20.

    //Side Spawn Location:
    private float spawnPosX = -22;   //Spawn at x 24.
    private float zMin = 2;     //Beginning of range to spawn on z axis.
    private float zMax = 15;    //End of range to spawn on z axis.

    //Timer control variables to spawn on a timer:
    private float startDelay = 2;   //Delays spawn in seconds from after scene starts when used in start method. 
    private float spawnInterval = 2f;     //Tells it how often to spawn once started.

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);    //Calls SpawnRandomAnimal() at start of scene based on startDelay variable and then spawns at intervals set by variable.
        InvokeRepeating("SpawnAnimalLeft", startDelay, spawnInterval);
        InvokeRepeating("SpawnAnimalRight", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        //*Commented all of this code out so it uses InvokeRepeating in Start() to spawn on a timer instead of a key press.
        //if (Input.GetKeyDown(KeyCode.S))    //Checks if S key is pressed.
        //{
        //*Copied & pasted all this code to create a custom method for SpawnRandomAnimal() so commented it out here to show changes:
        //Randomly generate animal index & spawn location:
        //Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);  //Spawns prefab at random range on x axis while keeping y & z axis position the same.
        //int animalIndex = Random.Range(0, animalPrefabs.Length);    //Generates a random number from 0 to the total number of prefabs in the array so it spawns either of the 3 prefab game objects.
        //Random.Range() method gives a random number within the range specified inside the () for example (0,100) would give a random number between 1 and 100 like 58 and differ each time called.

        //Instantiate(animalPrefabs[animalIndex], new Vector3(0, 0, 20), animalPrefabs[animalIndex].transform.rotation);  //Spawns a new animalprefab at the top of the screen when S key is pressed based on prefabs rotation.
        //Replaced hardcoded Vector3 with variable.
        //Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);   //Uses spawnPos variable set above to spawn at random locations.

        //SpawnRandomAnimal();    //Calls the custom method Written below.
        //}
    }

    void SpawnRandomAnimal()
    {
        //*Copied code from above:
        //Randomly generate animal index & spawn location:
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);  //Spawns prefab at random range on x axis while keeping y & z axis position the same.
        int animalIndex = Random.Range(0, animalPrefabs.Length);    //Generates a random number from 0 to the total number of prefabs in the array so it spawns either of the 3 prefab game objects.
                                                                    //Random.Range() method gives a random number within the range specified inside the () for example (0,100) would give a random number between 1 and 100 like 58 and differ each time called.

        //Instantiate(animalPrefabs[animalIndex], new Vector3(0, 0, 20), animalPrefabs[animalIndex].transform.rotation);  //Spawns a new animalprefab at the top of the screen based on prefabs rotation.
        //Replaced hardcoded Vector3 with variable.
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);   //Uses spawnPos variable set above to spawn at random locations & spawn random animalPrefab using animalIndex.
    }

    void SpawnAnimalLeft()
    {
        Vector3 rotation = new Vector3(0, 90, 0);   //Variable to store the hardcoded rotation value that will be used to turn the animal prefab to face right side.
        Vector3 spawnLeft = new Vector3(spawnPosX, 0, Random.Range(zMin, zMax));    //Spawns animal prefab at 24 on X axis at random position on z axis between 1-14.
        int animalIndex = Random.Range(0, animalPrefabs.Length);    //Reused this because it's only accessiable within the method.

        Instantiate(animalPrefabs[animalIndex], spawnLeft, Quaternion.Euler(rotation));   //Spawns animal prefab from left side & rotates it 90 on the y axis to run towards right side.
        //Quaternion.Euler is used to rotate game objects z degrees around z axis, x degrees around x axis, & y degrees around y axis; applied in that order.
    }

    void SpawnAnimalRight()
    {
        Vector3 rotation = new Vector3(0, -90, 0);  //Variable to store the hardcoded rotation value that will be used to turn the animal prefab to face left side.
        Vector3 spawnRight = new Vector3(-spawnPosX, 0, Random.Range(zMin, zMax));    //Spawns animal prefab at -24 on X axis at random position on z axis between 1-14.
        int animalIndex = Random.Range(0, animalPrefabs.Length);    //Reused this because it's only accessiable within the method.

        Instantiate(animalPrefabs[animalIndex], spawnRight, Quaternion.Euler(rotation));    //Spawns animal prefab from right side & rotates it -90 on the y axis to run towards left side.
    }
}

public class DetectCollisions : MonoBehaviour
{
    private GameManager gameManager;    //To reference GameManger.cs.

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   //Tells where to find gamemanager script.
    }

    private void OnTriggerEnter(Collider other)     //Built in method in MonoBehaviour class to detect collisions.
                                                    //Attach this script to anything that collides with something else and makes something happen on collision.
                                                    //Make sure to add colliders with Is Trigger checked to all game objects that will be effected when hit by something so it knows to test if something hits it.
                                                    //Also make sure to add a rigidbody to your projectile so it detects the collision with physics.
    {
        if (other.CompareTag("Player"))     //Checks if game object has the Tag set to Player.
        {
            //Debug.Log("Game Over!");    //If collides with player then show Game Over! in console.    //Removing to call GameManager script instead.
            gameManager.AddLives(-1);   //Removes 1 life if Player gets hit.
            Destroy(gameObject);    //Destroys gameObject that hits player.
        }
        else if (other.CompareTag("Animal"))    //Checks if colliding with a prefab taged as Animal. *Remember to set Tag in Inspector.
        {
            //gameManager.AddScore(5);    //Adds to score in GameManager.cs when animal is hit.
            //other.GetComponent<AnimalHunger>().FeedAnimal(1);   //Feeds the animal +1 every time it's hit.
            GetComponent<AnimalHunger>().FeedAnimal(1);     //Removed other. & it now works as it should.
            Destroy(other.gameObject);  //Destroys game object that hit it. (Whatever other game objects collider hits it.)
            //Destroy(gameObject);    //Destroys game object that script is attached to when hit by another collider.
        }
    }
}

public class GameManager : MonoBehaviour
{
    //Score & Lives Tracker:
    private int lives = 3;    //How many lives the player starts with.
    private int score = 0;    //The starting score.

    //These allow you to target on screen text to change:
    public TextMeshProUGUI livesDisplay;
    public TextMeshProUGUI scoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Lives = " + lives + " Score = " + score);
    }

    private void Update()
    {
        //These 2 tell you how to update on screen text:
        livesDisplay.text = "Lives: " + lives;
        scoreDisplay.text = "Score: " + score;
    }

    public void AddLives(int value)     //Takes in a value & updates it's variable.
    {
        lives += value;     //Tells it what variable to get value from and updates that variable.
        if (lives <= 0)     //Checks if lives are less than or equal to 0.
        {
            Debug.Log("Game Over!");    //If equal to 0 than displays Game Over! in console.
            lives = 0;  //Sets lives to 0 so it can't display a negative #. (Basically stopping the count at 0 when it's game over instead of constantly updating when game ends.)

            SceneManager.LoadScene(2);
        }
        else
        {
            //Debug.Log("Lives = " + lives);      //Displays lives remaining in console.
            Debug.Log("Lives = " + lives + " Score = " + score);
        }
    }

    public void AddScore(int value)     //Takes in a value & updates it's variable.
    {
        score += value;     //Tells it what variable to get value from and updates that variable.
        //Debug.Log("Score = " + score);  //Displays score in console.

        //Debug.Log("Lives = " + lives + " Score = " + score);

        //GameOver Function:
        if (lives <= 0)     //Checks if lives are less than or equal to 0.
        {
            Debug.Log("Game Over!");    //If equal to 0 than displays Game Over! in console.
            lives = 0;  //Sets lives to 0 so it can't display a negative #. (Basically stopping the count at 0 when it's game over instead of constantly updating when game ends.)

            SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log("Lives = " + lives + " Score = " + score);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        ApplicationQuit();
#endif
    }

}

public class AnimalHunger : MonoBehaviour
{
    //Using to give a health bar to the animals. Set up UI Slider prefab in inspector 1st.

    public Slider hungerSlider;     //Allows targeting a UI Slider GameObject.
    public int amountToBeFed;       //Tells how much can be fed before being destroyed.


    private int currentFedAmount = 0;   //Tells how much is already fed.

    private GameManager gameManager;    //Links to GameManager.cs.

    // Start is called before the first frame update
    void Start()
    {
        //Sets up the Start Slider position to the max value set by amountToBeFed variable:
        hungerSlider.maxValue = amountToBeFed;      //Sets slider max to amountToBeFed variable.
        hungerSlider.value = 0;     //Sliders starting value.
        hungerSlider.fillRect.gameObject.SetActive(false);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   //Tells where to find gamemanager script.


    }

    //Method to update the current fed amount of the animal:
    public void FeedAnimal(int amount)
    {
        currentFedAmount += amount;
        hungerSlider.fillRect.gameObject.SetActive(true);
        hungerSlider.value = currentFedAmount;

        if (currentFedAmount >= amountToBeFed)
        {
            gameManager.AddScore(amountToBeFed);    //Adds score in GameManager.
            Destroy(gameObject, 0.1f);      //Destroys projectile.
        }
    }
}
