using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPVFollowPlayer : MonoBehaviour
{
    public GameObject player;   // GameObject for camera to follow.
    private Vector3 offset = new Vector3(0, 4.3f, 0);   //Offset to position camera above player object.

    private float horizontalInput;  //Variable to store left, right input from player.
    private float turnSpeed = 50.0f;    //Speed to turn as player presses input.

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;    //Starting position of camera.
        horizontalInput = Input.GetAxis("Horizontal");  //Uses the Horizontal axis set in Unity to control left, right movement.
        transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);    //Rotates camera based on left, right player input times turnSpeed times once a second.
    }
}
