using UnityEngine;
using UnityEngine.UI;



public class SlidingPuzzle : MonoBehaviour

{

    public Button[] buttons; 

    private int[] puzzlePositions; 

    private int emptyIndex; // Index of the empty tile



    void Start()

    {

        // Initialize puzzle positions

        puzzlePositions = new int[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)

        {

            puzzlePositions[i] = i;

            buttons[i].onClick.AddListener(() => OnButtonClick(i)); 

        }



        // Shuffle puzzle

        ShufflePuzzle();

    }



    void ShufflePuzzle()

    {

        // Randomly shuffle puzzle positions

        for (int i = 0; i < puzzlePositions.Length; i++)

        {

            int temp = puzzlePositions[i];

            int randomIndex = Random.Range(i, puzzlePositions.Length);

            puzzlePositions[i] = puzzlePositions[randomIndex];

            puzzlePositions[randomIndex] = temp;

        }



        // Update button positions

        for (int i = 0; i < buttons.Length; i++)

        {

            buttons[i].transform.SetSiblingIndex(puzzlePositions[i]);

            if (puzzlePositions[i] == buttons.Length - 1)

            {

                emptyIndex = i;

            }

        }

    }



    void OnButtonClick(int buttonIndex)

    {

        // Check if clicked button can move into the empty space

        if (CanMove(buttonIndex))

        {

   

            int temp = puzzlePositions[buttonIndex];

            puzzlePositions[buttonIndex] = puzzlePositions[emptyIndex];

            puzzlePositions[emptyIndex] = temp;





            buttons[buttonIndex].transform.SetSiblingIndex(puzzlePositions[buttonIndex]);

            buttons[emptyIndex].transform.SetSiblingIndex(puzzlePositions[emptyIndex]);



            emptyIndex = buttonIndex; 

            Debug.Log("ButtonPressed"); 

        }

        if (IsPuzzleSolved())

        {

            Debug.Log("Puzzle solved!");

        }

    }



    bool CanMove(int buttonIndex)

    {

        // Check if clicked button is adjacent to the empty space

        return Mathf.Abs(buttonIndex / Mathf.Sqrt(buttons.Length) - emptyIndex / Mathf.Sqrt(buttons.Length)) +

               Mathf.Abs(buttonIndex % Mathf.Sqrt(buttons.Length) - emptyIndex % Mathf.Sqrt(buttons.Length)) == 1;

    }



    bool IsPuzzleSolved()

    {

        // Check if puzzle positions match initial positions

        for (int i = 0; i < buttons.Length; i++)

        {

            if (puzzlePositions[i] != i)

            {

                return false;

            }

        }

        return true;

    }

}

