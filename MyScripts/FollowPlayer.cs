using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
