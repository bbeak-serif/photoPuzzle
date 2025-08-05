using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    PuzzleGenerator generator;
    int puzzleSize;

    private void Awake() {
        generator = GetComponent<PuzzleGenerator>();
    }

    private void Start() {
        puzzleSize = GameManager.Instance.puzzleSize;

        generator.InitializePuzzleData(puzzleSize);
        generator.GeneratePuzzles();
    }
}
