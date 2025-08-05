using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class PuzzleGenerator : MonoBehaviour {
    [SerializeField] private bool isAvailableFlat = true;
    public int _puzzleSize;

    private PieceGenerator pieceGenerator;
    private int[,] horizontalSide;
    private int[,] verticalSide;
    private GameObject[,] puzzleObject;

    private PuzzleInitData saveData;

    private void Awake() {
        pieceGenerator = GetComponent<PieceGenerator>();
    }

    public void InitializePuzzleData(int puzzleSize) {
        _puzzleSize = puzzleSize;
        pieceGenerator.SetPuzzleSize(puzzleSize);
        horizontalSide = new int[puzzleSize + 1, puzzleSize];
        verticalSide = new int[puzzleSize, puzzleSize + 1];
        puzzleObject = new GameObject[puzzleSize, puzzleSize];
        pieceGenerator.SetPuzzleSize(puzzleSize);

        saveData = new PuzzleInitData(puzzleSize, horizontalSide, verticalSide);
    }

    public void LoadDataFromJson() {
        string path = Path.Combine(Application.persistentDataPath, "puzzleInit.json");
        if (File.Exists(path)) {
            string jsonData = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(jsonData, saveData);
        }
    }

    public void SaveDataToJson() {
        string jsonData = JsonUtility.ToJson(saveData);
        string path = Path.Combine(Application.persistentDataPath, "puzzleInit.json");
        File.WriteAllText(path, jsonData);
    }

    public void GeneratePuzzles() {
        for (int i = 0; i <= _puzzleSize; i++) {
            for (int j = 0; j < _puzzleSize; j++) {
                if(i == 0 || i == _puzzleSize) {
                    horizontalSide[i, j] = 0;
                }else {
                    horizontalSide[i, j] = Random.Range(isAvailableFlat ? 0 : 1, 3);
                }
            }
        }

        for(int i = 0; i < _puzzleSize; i++) {
            for(int j = 0; j <= _puzzleSize; j++) {
                if(j == 0 || j == _puzzleSize) {
                    verticalSide[i, j] = 0;
                } else {
                    verticalSide[i, j] = Random.Range(isAvailableFlat ? 0 : 1, 3);
                }
            }
        }

        for (int i = 0; i < _puzzleSize; i++) {
            for (int j = 0; j < _puzzleSize; j++) {
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

        SaveDataToJson();
        GeneratePuzzleObj();
    }

    private void GeneratePuzzleObj() {
        for (int i = 0; i < _puzzleSize; i++) {
            for (int j = 0; j < _puzzleSize; j++) {
                GameObject obj = puzzleObject[i, j];
                obj.transform.position = new Vector3(i, -j, 0);
                obj.transform.localScale = new Vector3(0.9f, 0.9f, 0);
                obj.name = "piece" + i + ", " + j;
            }
        }
    }
}
