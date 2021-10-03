using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
