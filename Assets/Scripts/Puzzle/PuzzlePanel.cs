using UnityEngine;

public class PuzzlePanel : MonoBehaviour
{
    [SerializeField, Range(0, 1)] float borderWidthRatio = 0.8f;
    //[SerializeField, Range(0, 1)] float borderHeightRatio = 0.5f;
    [SerializeField] float topGap = 0.2f;

    [Header("Grid Settings")]
    int gridSizeN = 3;  // �׸��� ũ�� n (n x n)
    [SerializeField] float gridGap = 0.1f;  // �׸��� �� gap (���� ����)
    [SerializeField] GameObject pieceHolder;  // ���簢�� prefab (SpriteRenderer ����)
    private void Start() {
        gridSizeN = GameManager.Instance.puzzleSize;

        AdjustBoardToScreen();
        CreateGrid();
    }

    private void AdjustBoardToScreen() {
        var topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        var worldSpaceWidth = topRightCorner.x * 2;
        var worldSpaceHeight = topRightCorner.y * 2;

        var spriteSize = GetComponent<SpriteRenderer>().bounds.size;

        var scaleFactorX = worldSpaceWidth / spriteSize.x;
        //var scaleFactorY = worldSpaceHeight / spriteSize.y;
        transform.localScale = new Vector3(scaleFactorX * borderWidthRatio, scaleFactorX * borderWidthRatio);// scaleFactorY * borderHeightRatio, 1);

        var screenTopY = topRightCorner.y;
        var gapDistance = topGap * worldSpaceHeight;
        var scaledSpriteHeight = spriteSize.y * transform.localScale.y; 
        transform.position = new Vector3(0, screenTopY - gapDistance - (scaledSpriteHeight / 2f), 0);
    }
    private void CreateGrid() {
        if (pieceHolder == null || gridSizeN <= 0) return;  // ���� ó��

        // �� ũ�� ���: gap ����, ���簢�� ����
        float length = 1f / gridSizeN;
        float startXPos = length / 2f - 0.5f;
        float startYPos = -startXPos;

        
        for (int i = 0; i < gridSizeN; i++) {
            for(int j = 0; j < gridSizeN; j++) {
                var square = Instantiate(pieceHolder, transform);
                square.transform.localScale = new Vector3(length, length, 1);

                square.transform.localPosition = new Vector3(startXPos + length * j, startYPos - length * i, 0);

            }
        }
    }
}
