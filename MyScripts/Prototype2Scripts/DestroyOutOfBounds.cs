using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
