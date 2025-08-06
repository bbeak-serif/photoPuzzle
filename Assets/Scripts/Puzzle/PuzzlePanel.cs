using UnityEngine;

public class PuzzlePanel : MonoBehaviour
{
    [SerializeField, Range(0, 1)] float borderWidthRatio = 0.8f;
    //[SerializeField, Range(0, 1)] float borderHeightRatio = 0.5f;
    [SerializeField] float topGap = 0.2f;

    [Header("Grid Settings")]
    int gridSizeN = 3;  // 그리드 크기 n (n x n)
    [SerializeField] float gridGap = 0.1f;  // 그리드 간 gap (월드 유닛)
    [SerializeField] GameObject pieceHolder;  // 정사각형 prefab (SpriteRenderer 포함)
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
        if (pieceHolder == null || gridSizeN <= 0) return;  // 예외 처리

        // 셀 크기 계산: gap 포함, 정사각형 유지
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
