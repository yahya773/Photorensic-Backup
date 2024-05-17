using UnityEngine;
using UnityEngine.UI;

public class SlidingImagePuzzle : MonoBehaviour
{
    public Button[] buttons; // Array of puzzle piece buttons
    public Image emptySpace; // Empty space image

    private int emptyIndex = 8; // Index of the empty space, initially set to 8
    private Sprite[] initialSprites; // Initial sprites for the puzzle pieces

    void Start()
    {
        StoreInitialSprites();

        // Swap images of elements 7 and 8 at the start
        SwapSprites(7, 8);

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    void StoreInitialSprites()
    {
        initialSprites = new Sprite[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            initialSprites[i] = buttons[i].image.sprite;
        }
    }

    void OnButtonClick(int clickedIndex)
    {
        if (CanMove(clickedIndex))
        {
            SwapSprites(clickedIndex, emptyIndex);
            emptyIndex = clickedIndex;

            if (IsPuzzleSolved())
            {
                Debug.Log("Puzzle solved!");
            }
        }
    }

    bool CanMove(int clickedIndex)
    {
        int rowClicked = clickedIndex / 3;
        int colClicked = clickedIndex % 3;
        int rowEmpty = emptyIndex / 3;
        int colEmpty = emptyIndex % 3;

        return Mathf.Abs(rowClicked - rowEmpty) + Mathf.Abs(colClicked - colEmpty) == 1;
    }

    void SwapSprites(int index1, int index2)
    {
        Sprite tempSprite = buttons[index1].image.sprite;
        buttons[index1].image.sprite = buttons[index2].image.sprite;
        buttons[index2].image.sprite = tempSprite;
    }

    bool IsPuzzleSolved()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].image.sprite != initialSprites[i])
            {
                return false;
            }
        }
        return true;
    }
}
