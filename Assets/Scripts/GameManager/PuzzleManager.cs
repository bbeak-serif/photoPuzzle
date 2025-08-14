using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

// �� �̻� �̱����� �ƴϹǷ�, �ٸ� ��ũ��Ʈ���� �����Ϸ��� FindObjectOfType�� ����ϰų�
// �ν����Ϳ��� ���� �Ҵ��ؾ� �մϴ�.
public class PuzzleManager : MonoBehaviour {
    [SerializeField] private PieceInventory inventory;
    [SerializeField] private GameObject clearPannel;

    // �� ��ũ��Ʈ�� ���� ������Ʈ�� PuzzleGenerator�� �ִٴ� ����
    private PuzzleGenerator generator;

    private bool[,] board;
    private int puzzleSize;
    private int correctNumberOfPuzzles;
    private void Awake() {
        generator = GetComponent<PuzzleGenerator>();
    }
    // ���� ������� ����

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
