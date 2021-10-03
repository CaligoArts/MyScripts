using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
