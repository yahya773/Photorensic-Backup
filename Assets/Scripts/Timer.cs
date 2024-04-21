using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public string nextSceneName;
    public float timeRemaining = 60;

    public GameObject timer2; 
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
     
        textMeshPro = timer2.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        
        textMeshPro.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
