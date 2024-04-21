using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; 
using UnityEngine;

public class CaseFile : MonoBehaviour
{
    [Header ("CaseFile")]
    public GameObject caseFile;

    [Header ("Subtitles Interact")] 
    public TMPro.TextMeshPro Subtitles;
    public string[] SubtitlesPlacement;
    public int placement;

    bool player_detection = false;

    public GameObject caseUI; 
    

    public void Start()
    {
        // caseFile.SetActive(false);
        caseUI.SetActive(false);
       caseFile = caseFile.GetComponent<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Trigger Collision"); 
        caseUI.SetActive(true); 
    }

    private void Update()
    {
        if (player_detection == true && Input.GetKeyDown(KeyCode.E))
        {
            placement = 1; 
           // caseFile.SetActive(true);
            Subtitles.text = SubtitlesPlacement[placement];
        }
    }
}
