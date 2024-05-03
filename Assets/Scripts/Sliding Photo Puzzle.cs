using UnityEngine;
using UnityEngine.UI; 

public class SlidingPhotoPuzzle : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private Sprite puzzleSprite;

    private GameObject[] pieces;
    private int emptyLocation;
    private int size;

    private void CreateGamePieces(float gapThickness)
    {
        float width = 1f / size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                GameObject piece = Instantiate(piecePrefab, gameTransform);

                piece.transform.localPosition = new Vector3(
                    -1f + (2f * width * col) + width,
                    1f - (2f * width * row) - width,
                    0f
                );

                piece.transform.localScale = ((2f * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";

                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.SetActive(false);
                }
                else
                {
                    SpriteRenderer spriteRenderer = piece.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = puzzleSprite;

                    BoxCollider2D boxCollider = piece.AddComponent<BoxCollider2D>();
                    boxCollider.size = new Vector2(width, width);
                }

                pieces[row * size + col] = piece;
            }
        }
    }

    void Start()
    {
        size = 3;
        pieces = new GameObject[size * size];
        CreateGamePieces(0.1f);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < pieces.Length; i++)
                {
                    if (pieces[i] == hit.transform.gameObject)
                    {
                        if (SwapIfValid(i, -size)) break;
                        if (SwapIfValid(i, size)) break;
                        if (SwapIfValid(i, -1)) break;
                        if (SwapIfValid(i, 1)) break;
                    }
                }
            }
        }
    }

    private bool SwapIfValid(int i, int offset)
    {
        if (((i + offset) >= 0) && ((i + offset) < pieces.Length) && (Mathf.Abs((i + offset) % size - i % size) <= 1))
        {
            if (i + offset == emptyLocation)
            {
                (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
                emptyLocation = i;
                return true;
            }
        }
        return false;
    }
}
