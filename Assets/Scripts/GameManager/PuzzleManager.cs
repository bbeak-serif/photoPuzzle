using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

// 더 이상 싱글턴이 아니므로, 다른 스크립트에서 참조하려면 FindObjectOfType을 사용하거나
// 인스펙터에서 직접 할당해야 합니다.
public class PuzzleManager : MonoBehaviour {
    [SerializeField] private PieceInventory inventory;
    [SerializeField] private GameObject clearPannel;

    // 이 스크립트와 같은 오브젝트에 PuzzleGenerator가 있다는 가정
    private PuzzleGenerator generator;

    private bool[,] board;
    private int puzzleSize;
    private int correctNumberOfPuzzles;
    private void Awake() {
        generator = GetComponent<PuzzleGenerator>();
    }
    // ▲▲▲ 여기까지 ▲▲▲

    private void Start() {
        if (GameManager.Instance.isNewGame) {
            puzzleSize = GameManager.Instance.puzzleSize;
            board = new bool[puzzleSize, puzzleSize];

            generator.InitializePuzzleData(puzzleSize);
            inventory.InitializePuzzleSize(puzzleSize);
            generator.GeneratePuzzlesData();
            inventory.GeneratePiecesInPanel();
        }
    }

    public void UpdateBoard(Vector2Int pos) {
        board[pos.x, pos.y] = true;
        correctNumberOfPuzzles++;

        if (correctNumberOfPuzzles == puzzleSize * puzzleSize)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel() {
        clearPannel.SetActive(true);
    }

    public void ToMenu() {
        GameManager.Instance.isNewGame = false;
        SceneManager.LoadScene(1);
    }
}
