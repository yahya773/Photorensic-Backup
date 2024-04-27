using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCInteractDialogue : MonoBehaviour
{
    public GameObject d_template;
    public GameObject canva;

    [Header("Sprites")] 
    public GameObject neutralSpriteFadeIn;
    public GameObject neutralSprite;
    public GameObject happySprite;
    public GameObject ExtraSprite;
    public GameObject surpriseSprite;
    public GameObject ExtraSpriteBlinking;

    public GameObject pressE;
    public GameObject template;

    [Header("Text Speed")] 
    public float textSpeed = 0.05f;

    [Header("Dialoge")] 
    public string[] Dialogue;
    public int placement;

    [Header("Text")]
    public TextMeshProUGUI text2TMP; // TextMeshPro for dialogue
    public TextMeshProUGUI text3TMP; // TextMeshPro for character name

    public string[] Name;
    public int Name2;

    [Header("Options")]
    public GameObject optionsPanel;
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;

    private bool optionsDisplayed;

    public bool player_detection; 

    void Start()
    {
        pressE.SetActive(true);
        template.SetActive(false);
        neutralSprite.SetActive(false);
        neutralSpriteFadeIn.SetActive(false);
        happySprite.SetActive(false);
        ExtraSprite.SetActive(false);
        ExtraSpriteBlinking.SetActive(false);
        surpriseSprite.SetActive(false);
        optionsPanel.SetActive(false);
        player_detection = false; 

        option1Button.onClick.AddListener(() => SelectOption(1));
        option2Button.onClick.AddListener(() => SelectOption(2));
        option3Button.onClick.AddListener(() => SelectOption(3));

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && player_detection)
        {
            pressE.SetActive(false);
            template.SetActive(true);
            canva.SetActive(true);
            SetCharacterName();
            // neutralSprite.SetActive(true);
            // neutralSpriteFadeIn.SetActive(true);
            StartCoroutine(TypeDialogue());
            Debug.Log("Collider Guy Thorpe");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_detection = true;
            pressE.SetActive(true); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_detection = false;
            pressE.SetActive(false); 
        }
    }

    IEnumerator TypeDialogue()
    {
        string currentText = Dialogue[placement]; // Get the dialogue text for the current placement
        string displayedText = "";
        int textIndex = 0;
        text2TMP.text = displayedText;




        while (textIndex < currentText.Length)
        {
            displayedText += currentText[textIndex];
            text2TMP.text = displayedText;
            textIndex++;
            yield return new WaitForSeconds(textSpeed);
        }

        if (Dialogue[placement].Contains("Hello!"))
        {
            ExtraSprite.SetActive(true);
            neutralSprite.SetActive(false);
            happySprite.SetActive(false);
            neutralSpriteFadeIn.SetActive(false);
        }


        // Typing finished, check for options to be displayed
        if (Dialogue[placement].Contains("I'm grumpy."))
        {
            happySprite.SetActive(true);
            ExtraSpriteBlinking.SetActive(false);
            neutralSprite.SetActive(false);
            ExtraSprite.SetActive(false);
            optionsPanel.SetActive(true);
            neutralSpriteFadeIn.SetActive(false);
            optionsDisplayed = true;
        }


        if (Dialogue[placement].Contains("Anyways, it would be a nice opportunity to put that fancy camera of yours to use!"))
        {
            happySprite.SetActive(true);
            ExtraSpriteBlinking.SetActive(false);
            neutralSpriteFadeIn.SetActive(false);
            neutralSprite.SetActive(false);
            ExtraSprite.SetActive(false);
            optionsPanel.SetActive(false);
        }

 

        // Increment placement if not currently typing options
        if (!optionsDisplayed && placement < Dialogue.Length)
        {
            placement++;
        }
    }


    void SetCharacterName()
    {
        text3TMP.text = Name[Name2];
        if (Dialogue[placement].Contains("He’s sent for us to investigate a break-in where a vase, said to be a precious family heirloom, was stolen."))
        {
            text3TMP.text = Name[Name2];
            neutralSpriteFadeIn.SetActive(false);
            Name2 = 1;
            Debug.Log("Change Name");

        }

     
    }
    public void SelectOption(int option)
    {
        switch (option)
        {
            case 1:
                Debug.Log("Option 1 selected");
                placement++;
                break;
            case 2:
                Debug.Log("Option 2 selected");
                placement++;
                break;
            case 3:
                Debug.Log("Option 3 selected");
                placement++;
                break;
            default:
                break;
        }

        // Hide options panel after selecting an option
        optionsPanel.SetActive(false);
        optionsDisplayed = false;

    }

}

