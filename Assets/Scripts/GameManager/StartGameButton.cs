using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartGameButton : MonoBehaviour {
    [SerializeField] private int puzzleSize = 16;

    private Button myButton;

    void Start() {
        myButton = GetComponent<Button>();

        myButton.onClick.RemoveAllListeners();
        myButton.onClick.AddListener(HandleClick);
    }

    void HandleClick() {
        if (GameManager.Instance != null) {
            GameManager.Instance.OnStartGame(puzzleSize);
        } else {
            Debug.LogError("GameManager.Instance is not found!");
        }
    }
}