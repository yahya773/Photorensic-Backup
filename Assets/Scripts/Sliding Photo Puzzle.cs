using UnityEngine;
using UnityEngine.UI;

public class SlidingPuzzle : MonoBehaviour
{
    public Texture2D puzzleImage; // The image to be used for the puzzle
    public int puzzleSize = 3; // Size of the puzzle (3x3 by default)
    public GameObject buttonPrefab; // Prefab for the puzzle piece buttons
    public Vector2 buttonSize = new Vector2(100f, 100f); // Size of each puzzle piece button

    private Texture2D[] puzzlePieces; // Array to hold individual puzzle pieces
    private int emptyIndex; // Index of the empty puzzle piece

    void Start()
    {
        // Split the puzzle image into pieces
        puzzlePieces = SplitImage(puzzleImage, puzzleSize);

        // Shuffle puzzle pieces
        ShufflePieces();

        // Create puzzle pieces buttons
        CreateButtons();
    }

    // Function to split the puzzle image into pieces
    private Texture2D[] SplitImage(Texture2D image, int size)
    {
        int pieceWidth = image.width / size;
        int pieceHeight = image.height / size;
        Texture2D[] pieces = new Texture2D[size * size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Texture2D piece = new Texture2D(pieceWidth, pieceHeight);
                piece.SetPixels(image.GetPixels(j * pieceWidth, (size - 1 - i) * pieceHeight, pieceWidth, pieceHeight));
                piece.Apply();
                pieces[i * size + j] = piece;
            }
        }

        return pieces;
    }

    // Function to shuffle the puzzle pieces
    private void ShufflePieces()
    {
        int length = puzzlePieces.Length;
        for (int i = 0; i < length; i++)
        {
            int randIndex = Random.Range(i, length);
            Texture2D temp = puzzlePieces[randIndex];
            puzzlePieces[randIndex] = puzzlePieces[i];
            puzzlePieces[i] = temp;

            if (temp == null)
                emptyIndex = i;
        }
    }

    // Function to create puzzle piece buttons
    private void CreateButtons()
    {
        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraintCount = puzzleSize;

        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.name = i.ToString();
            button.GetComponent<RectTransform>().sizeDelta = buttonSize;

            if (puzzlePieces[i] == null)
            {
                emptyIndex = i;
                continue;
            }

            Image image = button.GetComponent<Image>();
            image.sprite = Sprite.Create(puzzlePieces[i], new Rect(0, 0, puzzlePieces[i].width, puzzlePieces[i].height), new Vector2(0.5f, 0.5f));
            int index = i;
            button.GetComponent<Button>().onClick.AddListener(() => MovePiece(index));
        }
    }

    // Function to move puzzle piece
    private void MovePiece(int index)
    {
        if (IsAdjacent(index, emptyIndex))
        {
            // Swap pieces
            Texture2D temp = puzzlePieces[index];
            puzzlePieces[index] = null;
            puzzlePieces[emptyIndex] = temp;

            // Update empty index
            emptyIndex = index;

            // Update button images
            RefreshButtons();
        }
    }

    // Function to check if two pieces are adjacent
    private bool IsAdjacent(int index1, int index2)
    {
        int row1 = index1 / puzzleSize;
        int col1 = index1 % puzzleSize;
        int row2 = index2 / puzzleSize;
        int col2 = index2 % puzzleSize;

        return Mathf.Abs(row1 - row2) + Mathf.Abs(col1 - col2) == 1;
    }

    // Function to refresh button images
    private void RefreshButtons()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            Image image = transform.GetChild(i).GetComponent<Image>();
            if (puzzlePieces[i] != null)
                image.sprite = Sprite.Create(puzzlePieces[i], new Rect(0, 0, puzzlePieces[i].width, puzzlePieces[i].height), new Vector2(0.5f, 0.5f));
            else
                image.sprite = null;
        }
    }
}
