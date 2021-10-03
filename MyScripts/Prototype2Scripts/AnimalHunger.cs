using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //namespace Used to access the slider UI element.

public class AnimalHunger : MonoBehaviour
{
    //Using to give a health bar to the animals. Set up UI Slider prefab in inspector 1st.

    public Slider hungerSlider;     //Allows targeting a UI Slider GameObject.
    public int amountToBeFed;       //Tells how much can be fed before being destroyed.


    private int currentFedAmount = 0;   //Tells how much is already fed.

    private GameManager gameManager;    //Links to GameManager.cs.

    // Start is called before the first frame update
    void Start()
    {
        //Sets up the Start Slider position to the max value set by amountToBeFed variable:
        hungerSlider.maxValue = amountToBeFed;      //Sets slider max to amountToBeFed variable.
        hungerSlider.value = 0;     //Sliders starting value.
        hungerSlider.fillRect.gameObject.SetActive(false);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   //Tells where to find gamemanager script.

        
    }

    //Method to update the current fed amount of the animal:
    public void FeedAnimal(int amount)
    {
        currentFedAmount += amount;
        hungerSlider.fillRect.gameObject.SetActive(true);
        hungerSlider.value = currentFedAmount;

        if (currentFedAmount >= amountToBeFed)
        {
            gameManager.AddScore(amountToBeFed);    //Adds score in GameManager.
            Destroy(gameObject, 0.1f);      //Destroys projectile.
        }
    }
}
