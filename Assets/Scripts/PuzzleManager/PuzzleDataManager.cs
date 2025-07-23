using UnityEngine;
using System.IO;

public class PuzzleDataManager : MonoBehaviour {
    private PuzzleData currentPuzzleData;
    string savePath;

    private void Awake() {
        savePath = Application.persistentDataPath + "/puzzle_save.json";
    }

    public bool IsExistSaveData() {
        if (currentPuzzleData == null) return false;
        else return true;
    }

    public void SetPuzzleData(PuzzleData data) {
        currentPuzzleData = data;
        SavePuzzleData();
    }

    public PuzzleData GetPuzzleData() {
        return currentPuzzleData;
    }


    public void SavePuzzleData() {
        if (currentPuzzleData == null) return;

        string json = JsonUtility.ToJson(currentPuzzleData);
        File.WriteAllText(savePath, json);
        Debug.Log("The current puzzle data has been successfully saved");
    }

    public void LoadPuzzleData() {
        if (!File.Exists(savePath)) {
            Debug.LogError("Save data not found, would you like to create a new puzzle?");
            return;
        }

        string json = File.ReadAllText(savePath);
        currentPuzzleData = JsonUtility.FromJson<PuzzleData>(json);
    }

    public void LeavePuzzle() {
        if (File.Exists(savePath)) File.Delete(savePath);
        currentPuzzleData = null;
    }
}
