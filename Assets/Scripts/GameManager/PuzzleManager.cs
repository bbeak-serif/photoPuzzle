using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private PieceInventory inventory;
    PuzzleGenerator generator;
    int puzzleSize;

    private void Awake() {
        generator = GetComponent<PuzzleGenerator>();
    }

    private void Start() {
        if (GameManager.Instance.isNewGame) {
            puzzleSize = GameManager.Instance.puzzleSize;

            generator.InitializePuzzleData(puzzleSize);
            inventory.InitializePuzzleSize(puzzleSize);
            generator.GeneratePuzzlesData();
            inventory.GeneratePiecesInPanel();
        }

       
        
    }
}
