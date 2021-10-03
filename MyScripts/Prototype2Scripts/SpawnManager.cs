using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //*Create an Empty GameObject in Editor to attach SpawnManager script to with the same name.

    //Define which animal prefab spawns:
    public GameObject[] animalPrefabs;  //Allows assigning of an Array of GameObjects in Inspector.
    //public int animalIndex;     //Interger variable to use as a number value for which prefab is spawned. 0 spawns first prefab game object in array, 1 spawns 2nd in array, 2 spawns 3rd in array, & so on.
    //Removed Global variable because it's only needed locally in the if() statement below. Meaning nothing else needs to make use of the variable anywhere else in the script.

    //Define location of prefab spawns:
    //Top Spawn Location:
    private float spawnRangeX = 15;     //Sets spawn range on x axis to 20.
    private float spawnPosZ = 20;       //Sets position of where to spawn on z axis to 20.

    //Side Spawn Location:
    private float spawnPosX = -22;   //Spawn at x 24.
    private float zMin = 2;     //Beginning of range to spawn on z axis.
    private float zMax = 15;    //End of range to spawn on z axis.

    //Timer control variables to spawn on a timer:
    private float startDelay = 2;   //Delays spawn in seconds from after scene starts when used in start method. 
    private float spawnInterval = 2f;     //Tells it how often to spawn once started.

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);    //Calls SpawnRandomAnimal() at start of scene based on startDelay variable and then spawns at intervals set by variable.
        InvokeRepeating("SpawnAnimalLeft", startDelay, spawnInterval);
        InvokeRepeating("SpawnAnimalRight", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        //*Commented all of this code out so it uses InvokeRepeating in Start() to spawn on a timer instead of a key press.
        //if (Input.GetKeyDown(KeyCode.S))    //Checks if S key is pressed.
        //{
            //*Copied & pasted all this code to create a custom method for SpawnRandomAnimal() so commented it out here to show changes:
            //Randomly generate animal index & spawn location:
            //Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);  //Spawns prefab at random range on x axis while keeping y & z axis position the same.
            //int animalIndex = Random.Range(0, animalPrefabs.Length);    //Generates a random number from 0 to the total number of prefabs in the array so it spawns either of the 3 prefab game objects.
            //Random.Range() method gives a random number within the range specified inside the () for example (0,100) would give a random number between 1 and 100 like 58 and differ each time called.

            //Instantiate(animalPrefabs[animalIndex], new Vector3(0, 0, 20), animalPrefabs[animalIndex].transform.rotation);  //Spawns a new animalprefab at the top of the screen when S key is pressed based on prefabs rotation.
            //Replaced hardcoded Vector3 with variable.
            //Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);   //Uses spawnPos variable set above to spawn at random locations.

            //SpawnRandomAnimal();    //Calls the custom method Written below.
        //}
    }

    void SpawnRandomAnimal()
    {
        //*Copied code from above:
        //Randomly generate animal index & spawn location:
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);  //Spawns prefab at random range on x axis while keeping y & z axis position the same.
        int animalIndex = Random.Range(0, animalPrefabs.Length);    //Generates a random number from 0 to the total number of prefabs in the array so it spawns either of the 3 prefab game objects.
                                                                    //Random.Range() method gives a random number within the range specified inside the () for example (0,100) would give a random number between 1 and 100 like 58 and differ each time called.

        //Instantiate(animalPrefabs[animalIndex], new Vector3(0, 0, 20), animalPrefabs[animalIndex].transform.rotation);  //Spawns a new animalprefab at the top of the screen based on prefabs rotation.
        //Replaced hardcoded Vector3 with variable.
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);   //Uses spawnPos variable set above to spawn at random locations & spawn random animalPrefab using animalIndex.
    }

    void SpawnAnimalLeft()
    {
        Vector3 rotation = new Vector3(0, 90, 0);   //Variable to store the hardcoded rotation value that will be used to turn the animal prefab to face right side.
        Vector3 spawnLeft = new Vector3(spawnPosX, 0, Random.Range(zMin, zMax));    //Spawns animal prefab at 24 on X axis at random position on z axis between 1-14.
        int animalIndex = Random.Range(0, animalPrefabs.Length);    //Reused this because it's only accessiable within the method.

        Instantiate(animalPrefabs[animalIndex], spawnLeft, Quaternion.Euler(rotation));   //Spawns animal prefab from left side & rotates it 90 on the y axis to run towards right side.
        //Quaternion.Euler is used to rotate game objects z degrees around z axis, x degrees around x axis, & y degrees around y axis; applied in that order.
    }

    void SpawnAnimalRight()
    {
        Vector3 rotation = new Vector3(0, -90, 0);  //Variable to store the hardcoded rotation value that will be used to turn the animal prefab to face left side.
        Vector3 spawnRight = new Vector3(-spawnPosX, 0, Random.Range(zMin, zMax));    //Spawns animal prefab at -24 on X axis at random position on z axis between 1-14.
        int animalIndex = Random.Range(0, animalPrefabs.Length);    //Reused this because it's only accessiable within the method.

        Instantiate(animalPrefabs[animalIndex], spawnRight, Quaternion.Euler(rotation));    //Spawns animal prefab from right side & rotates it -90 on the y axis to run towards left side.
    }
}
