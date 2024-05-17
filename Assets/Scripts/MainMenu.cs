using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    public Scene GoToNextScene;
    public Animator FadeOut; 

    public void LoadGame() 
    {
        Fade
        SceneManager.LoadScene(1);
        Debug.Log("Go to MAP"); 
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QuitGame"); 
    }
   
}
