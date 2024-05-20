using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator fadeOutAnimator; // Animator for fade-out animation
    public float fadeDuration = 1f; // Duration of the fade-out animation

    // Method to load the game scene
    public void LoadGame()
    {
        StartCoroutine(FadeAndLoadScene(1)); // Change 1 to the desired scene index or name
    }

    // Method to quit the game
    public void QuitGame()
    {
        StartCoroutine(FadeAndQuitGame());
    }

    // Coroutine to handle the fade-out animation and scene loading
    IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        // Trigger the fade-out animation
        fadeOutAnimator.SetTrigger("FadeOutTrigger");

        // Wait until the fade-out animation is completed
        yield return new WaitForSeconds(fadeDuration);

        // Load the specified scene
        SceneManager.LoadScene(sceneIndex);
        Debug.Log("Go to MAP");
    }

    // Coroutine to handle the fade-out animation and quitting the game
    IEnumerator FadeAndQuitGame()
    {
        // Trigger the fade-out animation
        fadeOutAnimator.SetTrigger("FadeOutTrigger");

        // Wait until the fade-out animation is completed
        yield return new WaitForSeconds(fadeDuration);

        // Quit the application
        Application.Quit();
        Debug.Log("QuitGame");
    }
}
