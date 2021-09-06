using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OncomingVehicles : MonoBehaviour
    //Written for Prototype1 Bonus Challenges:
{
    private float speed = 5.0f; //Controls speed of oncoming vehicles.

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);  //Moves object script is attached to forward x speed x ever second.
    }
}
