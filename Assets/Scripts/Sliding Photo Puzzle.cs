using UnityEngine;
using UnityEngine.UI;

public class SlidingPhotoPuzzle : MonoBehaviour
{
    public Button[] buttons; // Array of puzzle piece buttons
    public Image[] puzzleImages; // Array of puzzle piece images
    public Image emptyPiece; // Image representing the empty space

    private int emptyIndex; // Index of the empty space
    private int[] puzzlePositions; // Array to hold puzzle piece indices

    void Start()
    {
        // Initialize puzzle positions array
        puzzlePositions = new int[buttons.Length];

        // Assign puzzle piece images and set up button click listeners
        for (int i = 0; i < buttons.Length; i++)
        {
            puzzlePositions[i] = i;
            buttons[i].image.sprite = puzzleImages[i].sprite;
            int index = i; // Capture current value of i
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }

        // Set initial empty index
        emptyIndex = buttons.Length - 1;

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
        RefreshButtonPositions();

        // Check if puzzle is already solved
        if (IsPuzzleSolved())
        {
            // If solved, shuffle again
            ShufflePuzzle();
        }
    }

    void OnButtonClick(int buttonIndex)
    {
        // Check if clicked button can move into the empty space
        if (CanMove(buttonIndex))
        {
            // Swap button indices
            int temp = puzzlePositions[buttonIndex];
            puzzlePositions[buttonIndex] = puzzlePositions[emptyIndex];
            puzzlePositions[emptyIndex] = temp;

            // Update empty index
            emptyIndex = buttonIndex;

            // Update button positions
            RefreshButtonPositions();

            // Check if puzzle is solved
            if (IsPuzzleSolved())
            {
                Debug.Log("Puzzle solved!");
            }
        }
    }

    bool CanMove(int buttonIndex)
    {
        // Check if clicked button is adjacent to the empty space
        if (buttonIndex % 3 != 0 && puzzlePositions[buttonIndex - 1] == buttons.Length - 1) // Check left
            return true;
        if (buttonIndex % 3 != 2 && puzzlePositions[buttonIndex + 1] == buttons.Length - 1) // Check right
            return true;
        if (buttonIndex / 3 != 0 && puzzlePositions[buttonIndex - 3] == buttons.Length - 1) // Check up
            return true;
        if (buttonIndex / 3 != 2 && puzzlePositions[buttonIndex + 3] == buttons.Length - 1) // Check down
            return true;

        return false;
    }

    void RefreshButtonPositions()
    {
        // Update button positions based on current puzzle positions
        for (int i = 0; i < buttons.Length; i++)
        {
            int puzzleIndex = puzzlePositions[i];
            Vector3 newPosition = buttons[puzzleIndex].transform.localPosition;
            buttons[i].transform.localPosition = newPosition;
        }
    }

    bool IsPuzzleSolved()
    {
        // Check if puzzle positions match the correct order
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
