using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


namespace Photorensic
{
    public class NPCSystem : MonoBehaviour
    {
        public GameObject d_template;
        public GameObject canva;

        public GameObject neutralSpriteFadeIn; 
        public GameObject neutralSprite;
        public GameObject happySprite;
        public GameObject ExtraSprite;
        public GameObject surpriseSprite;
        public GameObject ExtraSpriteBlinking;

        public GameObject pressE;
        public GameObject template;

        public GameObject caseFile;
        public GameObject caseFile2; 

        public float textSpeed = 0.05f; 

        public string[] Dialogue;
        public int placement;

        public TextMeshProUGUI text2TMP; // TextMeshPro for dialogue
        public TextMeshProUGUI text3TMP; // TextMeshPro for character name

        public string[] Name;
        public int Name2;

        public string nextSceneName;

        [Header("Options")]
        public GameObject optionsPanel;
        public Button option1Button;
        public Button option2Button;
        public Button option3Button;
        private bool optionsDisplayed;

        [Header("Skip")]
        public GameObject MiscellionousPanel; 
        public Button skip;
        public Button Reverse; 

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
            MiscellionousPanel.SetActive(false);
            caseFile.SetActive(false);
            caseFile2.SetActive(false); 



            option1Button.onClick.AddListener(() => SelectOption(1));
            option2Button.onClick.AddListener(() => SelectOption(2));
            option3Button.onClick.AddListener(() => SelectOption(3));

            skip.onClick.AddListener(() => Skip(1));
            Reverse.onClick.AddListener(() => Skip(2)); 
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MiscellionousPanel.SetActive(true);
                pressE.SetActive(false);
                template.SetActive(true); 
                canva.SetActive(true);
                SetCharacterName();
                // neutralSprite.SetActive(true);
                // neutralSpriteFadeIn.SetActive(true);
                StartCoroutine(TypeDialogue());

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

            if (Dialogue[placement].Contains("So…"))
            {
                ExtraSprite.SetActive(true);
                neutralSprite.SetActive(false);
                happySprite.SetActive(false);
                neutralSpriteFadeIn.SetActive(false);
                caseFile.SetActive(true); 
            }

            if (Dialogue[placement].Contains("Right, newbie!"))
            {
                ExtraSprite.SetActive(false);
                neutralSprite.SetActive(false);
                happySprite.SetActive(false);
                neutralSpriteFadeIn.SetActive(true); 
            }

            if (Dialogue[placement].Contains("It’s a man in his late 60s. Sir Guy Thorpe."))
            {
                ExtraSpriteBlinking.SetActive(true);
                neutralSprite.SetActive(false);
                ExtraSprite.SetActive(false);
                caseFile2.SetActive(true);
                caseFile.SetActive(false); 
            }

            // Typing finished, check for options to be displayed
            if (Dialogue[placement].Contains("Excited for your first proper case?"))
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

            if (Dialogue[placement].Contains("What do you mean? "))
            {
                happySprite.SetActive(false);
                ExtraSpriteBlinking.SetActive(false);
                neutralSpriteFadeIn.SetActive(false);
                neutralSprite.SetActive(false);
                ExtraSprite.SetActive(true);
                surpriseSprite.SetActive(false);
            }

            if (Dialogue[placement].Contains("Oh!"))
            {
                happySprite.SetActive(false);
                ExtraSpriteBlinking.SetActive(false);
                neutralSprite.SetActive(false);
                neutralSpriteFadeIn.SetActive(false);
                ExtraSprite.SetActive(false);
                surpriseSprite.SetActive(true); 
            }

            if (Dialogue[placement].Contains("(unimpressed / questioning) Right… Like this place has even been open for longer than a year."))
            {
                happySprite.SetActive(true);
                ExtraSpriteBlinking.SetActive(false);
                neutralSpriteFadeIn.SetActive(false);
                neutralSprite.SetActive(false);
                ExtraSprite.SetActive(false);
                surpriseSprite.SetActive(false);
            }

            if (Dialogue[placement].Contains("I wonder why he hired us specifically… "))
            {
                happySprite.SetActive(false);
                ExtraSpriteBlinking.SetActive(false);
                neutralSpriteFadeIn.SetActive(false);
                neutralSprite.SetActive(false);
                ExtraSprite.SetActive(false);
                surpriseSprite.SetActive(true);
                caseFile.SetActive(false);
                caseFile2.SetActive(false); 
            }



            if (Dialogue[placement].Contains("Let’s get the lead out!"))
            {
                SceneManager.LoadScene(nextSceneName);
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

            if (Dialogue[placement].Contains("Could he not just… replace it? Sounds rich enough."))
            {
                text3TMP.text = Name[Name2];
                neutralSpriteFadeIn.SetActive(false);
                Name2 = 0;
                Debug.Log("Change Name");

            }

            if (Dialogue[placement].Contains("What do you mean?"))
            {
                text3TMP.text = Name[Name2];
                Name2 = 1;
                neutralSpriteFadeIn.SetActive(false);
                Debug.Log("Change Name"); 
            }

            if (Dialogue[placement].Contains("You’re probably just that good."))
            {
                text3TMP.text = Name[Name2];
                neutralSpriteFadeIn.SetActive(false);
                Name2 = 0;
                Debug.Log("Change Name");
            }

            if (Dialogue[placement].Contains("I wonder why he hired us specifically… "))
            {
                text3TMP.text = Name[Name2];
                neutralSpriteFadeIn.SetActive(false);
                Name2 = 1;
                Debug.Log("Change Name");
            }

            if (Dialogue[placement].Contains("I doubt anyone referred to him. Maybe he sought us out. "))
            {
                text3TMP.text = Name[Name2];
                neutralSpriteFadeIn.SetActive(false);
                Name2 = 0;
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

        public void Skip(int skip)
        {
            switch(skip)
            {
                case 1:
                    Debug.Log("Go Back");
                    placement -= 2; 
                    if (placement < 0)
                    {
                        placement = 0; 
                    }
                    break;
                case 2:
                    Debug.Log("Skip Dialogue");
                    placement = 20; 
                    break;
                default:
                    break; 
               

            }
        }

    }
}
