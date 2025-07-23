using UnityEngine;
using System.Collections.Generic;

public class PuzzleGenerator : MonoBehaviour {
    [SerializeField] private int puzzleSize;
    [SerializeField] private bool isAvailableFlat = true;

    private PieceGenerator pieceGenerator;
    private int[,] horizontalSide;
    private int[,] verticalSide;
    private GameObject[,] puzzleObject;
    private List<PieceData> pieceData;
    private PuzzleData puzzleData;

    private void Awake() {
        InitializePuzzleData();
    }

    private void InitializePuzzleData() {
        puzzleData = null;
        pieceGenerator.SetPuzzleSize(puzzleSize);
        pieceData = new List<PieceData>();
        horizontalSide = new int[puzzleSize + 1, puzzleSize];
        verticalSide = new int[puzzleSize, puzzleSize + 1];
        puzzleObject = new GameObject[puzzleSize, puzzleSize];
    }

    public PuzzleData GeneratePuzzles() {
        InitializePuzzleData();
        for (int i = 0; i <= puzzleSize; i++) {
            for (int j = 0; j < puzzleSize; j++) {
                if(i == 0 || i == puzzleSize) {
                    horizontalSide[i, j] = 0;
                }else {
                    horizontalSide[i, j] = Random.Range(isAvailableFlat ? 0 : 1, 3);
                }
            }
        }

        for(int i = 0; i < puzzleSize; i++) {
            for(int j = 0; j <= puzzleSize; j++) {
                if(j == 0 || j == puzzleSize) {
                    verticalSide[i, j] = 0;
                } else {
                    verticalSide[i, j] = Random.Range(isAvailableFlat ? 0 : 1, 3);
                }
            }
        }

        for (int i = 0; i < puzzleSize; i++) {
            for (int j = 0; j < puzzleSize; j++) {
                //int bottomType, int leftType, int topType, int rightType
                if (i == 0 && j == 0){
                    puzzleObject[i, j] = pieceGenerator.GeneratePieceMesh(verticalSide[i, j + 1], horizontalSide[i, j], verticalSide[i, j], horizontalSide[i + 1, j], new Vector2(i, j));
                } else if(i != 0 && j == 0) {
                    puzzleObject[i, j] = pieceGenerator.GeneratePieceMesh(verticalSide[i, j + 1], (horizontalSide[i, j] == 0) ? 0 : 3 - horizontalSide[i, j], verticalSide[i, j], horizontalSide[i + 1, j], new Vector2(i, j));
                } else if(i == 0 && j != 0) {
                     puzzleObject[i, j] = pieceGenerator.GeneratePieceMesh(verticalSide[i, j + 1], horizontalSide[i, j], (verticalSide[i, j] == 0) ? 0 : 3 - verticalSide[i, j], horizontalSide[i + 1, j], new Vector2(i, j));
                } else {
                    puzzleObject[i, j] = pieceGenerator.GeneratePieceMesh(verticalSide[i, j + 1], (horizontalSide[i, j] == 0) ? 0 : 3 - horizontalSide[i, j], (verticalSide[i, j] == 0) ? 0 : 3 - verticalSide[i, j], horizontalSide[i + 1, j], new Vector2(i, j));
                }
            }
        }

        GeneratePuzzleObj();
        return puzzleData;
    }

    public void GeneratePuzzleBySaveData(PuzzleData data) {
        puzzleSize = data.puzzleSize;
        InitializePuzzleData();
        puzzleData = data;
        int i = 0;

        foreach (PieceData piece in puzzleData.pieces) {
            int col = i % puzzleSize == 0 ? 3 : i % puzzleSize;
            int row = i % puzzleSize == 0 ? (int)i / puzzleSize - 1 : (int)i / puzzleSize;

            puzzleObject[row, col] = pieceGenerator.GeneratePieceMesh(piece.bottomType, piece.leftType, piece.topType, piece.rightType, piece.CorrectPosition);

            i++;
        }
    }

    private void GeneratePuzzleObj() {
        for (int i = 0; i < puzzleSize; i++) {
            for (int j = 0; j < puzzleSize; j++) {
                GameObject obj = puzzleObject[i, j];
                obj.transform.position = new Vector3(i, -j, 0);
                obj.transform.localScale = new Vector3(0.9f, 0.9f, 0);
                obj.name = "piece" + i + ", " + j;
                pieceData.Add(obj.GetComponent<PieceData>());
            }
        }

        puzzleData = new PuzzleData();
        puzzleData.pieces = pieceData;
        puzzleData.puzzleSize = puzzleSize;
    }
}
