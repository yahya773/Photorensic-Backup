using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace Photorensic
{
    public class NPCSystem : MonoBehaviour
    {
        public GameObject d_template;
        public GameObject canva;

        public GameObject neutralSprite;
        public GameObject happySprite;
        public GameObject ExtraSprite;
        public GameObject surpriseSprite;
        public GameObject ExtraSpriteBlinking; 

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

        void Start()
        {
            neutralSprite.SetActive(false);
            happySprite.SetActive(false);
            ExtraSprite.SetActive(false);
            ExtraSpriteBlinking.SetActive(false); 
            surpriseSprite.SetActive(false);
            optionsPanel.SetActive(false);

            option1Button.onClick.AddListener(() => SelectOption(1));
            option2Button.onClick.AddListener(() => SelectOption(2));
            option3Button.onClick.AddListener(() => SelectOption(3));
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                canva.SetActive(true);
                SetCharacterName();
                neutralSprite.SetActive(true);
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
            }

            if (Dialogue[placement].Contains("It’s a man in his late 60s. Sir Guy Thorpe."))
            {
                ExtraSpriteBlinking.SetActive(true);
                neutralSprite.SetActive(false);
                ExtraSprite.SetActive(false); 
            }

            // Typing finished, check for options to be displayed
            if (Dialogue[placement].Contains("Excited for your first proper case?"))
            {
                happySprite.SetActive(true);
                ExtraSpriteBlinking.SetActive(false); 
                neutralSprite.SetActive(false);
                ExtraSprite.SetActive(false); 
                optionsPanel.SetActive(true);
                optionsDisplayed = true;
            }

            if (Dialogue[placement].Contains("Could he not just… replace it? Sounds rich enough."))
            {
                neutralSprite.SetActive(false);
                happySprite.SetActive(false);
                ExtraSprite.SetActive(false);
                ExtraSpriteBlinking.SetActive(false);
                surpriseSprite.SetActive(false);
                optionsPanel.SetActive(false);

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
            if (Dialogue[placement].Contains("Could he not just"))
            {
                text3TMP.text = Name[Name2];
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
}
