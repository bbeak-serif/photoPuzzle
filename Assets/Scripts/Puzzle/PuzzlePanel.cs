using UnityEngine;
using UnityEngine.UI;

public class PuzzlePanel : MonoBehaviour
{
    [Header("Grid Settings")]
    int gridSizeN = 3;
    [SerializeField] float gridGap = 0.1f;
    [SerializeField] GameObject pieceHolder;
    RectTransform gridContainer;
    GridLayoutGroup gridLayoutGroup;

    private void Start() {
        gridSizeN = GameManager.Instance.puzzleSize;
        gridContainer = GetComponent<RectTransform>();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        
        CreateGrid();
    }

    private void CreateGrid() {
        if (pieceHolder == null || gridSizeN <= 0) return;

        float containerWidth = gridContainer.rect.width;
        float cellSize = gridContainer.rect.width / gridSizeN;
        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);

        for (int i = 0; i < gridSizeN*gridSizeN; i++) {
            Instantiate(pieceHolder, gridContainer);
        }
    }
}
