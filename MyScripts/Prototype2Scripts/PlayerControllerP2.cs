using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
