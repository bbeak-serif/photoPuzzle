using UnityEngine;

[RequireComponent(typeof(PuzzleDataManager), typeof(PuzzleGenerator))]
public class PuzzleManager : MonoBehaviour {
    private PuzzleDataManager puzzleDataManager;
    private PuzzleGenerator puzzleGenerator;

    private void Awake() {
        puzzleDataManager = GetComponent<PuzzleDataManager>();
        puzzleGenerator = GetComponent<PuzzleGenerator>();
    }

    public void CreateNewPuzzle() {
        if (puzzleDataManager.IsExistSaveData()) {
            Debug.LogError("Save data already exist, would you like to load the save data?");
            return;
        }

        puzzleDataManager.SetPuzzleData(puzzleGenerator.GeneratePuzzles());
    }

    public void LoadPuzzle() {
        puzzleDataManager.LoadPuzzleData();
        puzzleGenerator.GeneratePuzzleBySaveData(puzzleDataManager.GetPuzzleData());
    }

    public void LeavePuzzle() {

    }
}
