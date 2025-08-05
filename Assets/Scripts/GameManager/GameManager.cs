using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour {
    private static GameManager instance;
    public static GameManager Instance {
        get {
            if (instance == null) instance = new GameManager();
            return instance;
        }
    }
    public int puzzleSize;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void OnStartGame(int puzzleSize) {
        Debug.Log(Application.persistentDataPath + "/SourceImage");
        if (!Directory.Exists(Application.persistentDataPath + "/SourceImage")) return;
        this.puzzleSize = puzzleSize;
        SceneManager.LoadScene(1);
        
    }
}

