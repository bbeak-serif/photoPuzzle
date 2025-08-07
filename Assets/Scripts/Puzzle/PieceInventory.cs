using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PieceInventory : MonoBehaviour
{
    [SerializeField] private PuzzleGenerator generator;
    [SerializeField] private GridLayoutGroup gridLayout; // 패널의 Grid Layout Group
    private int puzzleSize = 3; // n x n

    private List<GameObject> pieces = new List<GameObject>();

    private void Start() {
        //GeneratePiecesInPanel();
        //ShufflePieces();
    }

    public void InitializePuzzleSize(int size) {
        puzzleSize = size;
    }

    public void GeneratePiecesInPanel() {
        var pieceObjs = generator.GeneratePuzzleObj();
        for (int row = 0; row < puzzleSize; row++) {
            for (int col = 0; col < puzzleSize; col++) {
                GameObject piece = pieceObjs[row,col];
                Debug.Log(piece.name);
                piece.transform.SetParent(gridLayout.transform);
                piece.name = "Piece_" + row + "_" + col;
                pieces.Add(piece);
            }
        }

        ShufflePieces();
    }

    public void ShufflePieces() {
        // 조각 위치 셔플 (GridLayoutGroup에서 자식 순서 변경)
        for (int i = pieces.Count - 1; i > 0; i--) {
            int randomIndex = Random.Range(0, i + 1);
            pieces[i].transform.SetSiblingIndex(randomIndex);
        }
    }
}
