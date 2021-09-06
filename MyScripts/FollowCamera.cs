using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
