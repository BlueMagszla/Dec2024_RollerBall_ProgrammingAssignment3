using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public void startButtonClicked() //starts the game, loads scene 1
    {
        SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
    }

    public void optionsButtonClicked() //prints options is clicked, manages menus in editor
    {
        print("Options was clicked.");
    }

    public void quitButtonClicked() //exits application
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit(0);
#endif
        //Editor Macros
    }
}
