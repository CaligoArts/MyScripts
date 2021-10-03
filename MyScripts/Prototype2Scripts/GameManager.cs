using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Score & Lives Tracker:
    private int lives = 3;    //How many lives the player starts with.
    private int score = 0;    //The starting score.

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Lives = " + lives + " Score = " + score);
    }

    public void AddLives(int value)     //Takes in a value & updates it's variable.
    {
        lives += value;     //Tells it what variable to get value from and updates that variable.
        if (lives <= 0)     //Checks if lives are less than or equal to 0.
        {
            Debug.Log("Game Over!");    //If equal to 0 than displays Game Over! in console.
            lives = 0;  //Sets lives to 0 so it can't display a negative #. (Basically stopping the count at 0 when it's game over instead of constantly updating when game ends.)
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
        }
        else
        {
            Debug.Log("Lives = " + lives + " Score = " + score);
        }
    }
}