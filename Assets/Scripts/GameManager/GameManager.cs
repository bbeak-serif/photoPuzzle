using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public int puzzleSize;
    public bool isNewGame;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void OnStartGame(int puzzleSize) {
        if (!Directory.Exists(Application.persistentDataPath + "/SourceImage")) return;
        this.puzzleSize = puzzleSize;
        this.isNewGame = true;
        SceneManager.LoadScene(2);
    }
}

