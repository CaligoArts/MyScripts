using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTransform : MonoBehaviour
{
    // Vector3 is a data type that stores 3 values.
    public Vector3 scaleChange;     // This variable will store the x, y, z values for scale to change size of gameObject.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
        // There are roughly 24 frames per second so setting a transform under Update would be multiplying values 24 times every second.
    {
        transform.localScale += scaleChange;
        // transform.   Refers to Transform component of your GameObject.
        // localScale   Refers to Scale of the GameObject.
        // +=   Is an Operator that adds values set in scaleChange variable to the current scale values to make the ball grow.


    }
}
