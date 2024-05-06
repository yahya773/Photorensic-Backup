using UnityEngine;
using UnityEngine.UI;

public class SlidingPuzzle : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Sprite[] puzzleSprites; // Array of puzzle sprites in order

    private GameObject[] buttons;
    private int size = 3; // Change this to adjust puzzle size
    private int emptyIndex;

    void Start()
    {
        CreatePuzzle();
    }

    void CreatePuzzle()
    {
        int puzzleLength = size * size;

        // Create buttons array
        buttons = new GameObject[puzzleLength];

        // Calculate button size
        float buttonWidth = gridLayoutGroup.cellSize.x;
        float buttonHeight = gridLayoutGroup.cellSize.y;

        // Instantiate buttons and set sprite
        for (int i = 0; i < puzzleLength; i++)
        {
            GameObject button = Instantiate(buttonPrefab, gridLayoutGroup.transform);
            int index = i; // Capture the index value
            button.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(index)); // Add click listener

            // Set sprite
            Image image = button.GetComponent<Image>();
            int spriteIndex = (i == puzzleLength - 1) ? puzzleLength - 1 : i % puzzleSprites.Length;
            image.sprite = puzzleSprites[spriteIndex];

            // Set button name and position
            button.name = i.ToString();
            buttons[i] = button;

            // Set empty index
            if (i == puzzleLength - 1)
                emptyIndex = i;
        }
    }

    void OnButtonClick(int index)
    {
        if (IsAdjacent(index, emptyIndex))
        {
            SwapButtons(index, emptyIndex);
            emptyIndex = index;
        }
    }

    bool IsAdjacent(int index1, int index2)
    {
        int row1 = index1 / size;
        int col1 = index1 % size;
        int row2 = index2 / size;
        int col2 = index2 % size;

        return (Mathf.Abs(row1 - row2) + Mathf.Abs(col1 - col2) == 1);
    }

    void SwapButtons(int index1, int index2)
    {
        // Swap button positions
        Vector3 tempPosition = buttons[index1].transform.localPosition;
        buttons[index1].transform.localPosition = buttons[index2].transform.localPosition;
        buttons[index2].transform.localPosition = tempPosition;

        // Swap buttons in the array
        GameObject tempButton = buttons[index1];
        buttons[index1] = buttons[index2];
        buttons[index2] = tempButton;
    }
}
