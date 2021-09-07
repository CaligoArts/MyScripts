using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //Gives ability to switch between scenes.
using UnityEngine.UI;   //Access UI elements.

//If using Editor then it applies the code to quit testing otherwise is applies code to exit built game:
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
    //Create an Empty GameObject to attach script to so you can target it in the OnClick function of the Button.
    //** Must include namespaces at top:
    //using UnityEngine.SceneManagement;  //Gives ability to switch between scenes.
    //using UnityEngine.UI;   //Access UI elements.

    //If using Editor then it applies the code to quit testing otherwise is applies code to exit built game:
    //#if UNITY_EDITOR
    //using UnityEditor;
    //#endif

{
    //These are Methods for Buttons to access to give them scene control:
    public void OnePlayer()
    {
        SceneManager.LoadScene(1);
    }

    public void TwoPlayer()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();   //Exits Playmode when using Editor.
#else
        Application.Quit();     //Exits application after built.
#endif
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
