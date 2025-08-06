using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    void SaveData() {
        PuzzleInitData d = new PuzzleInitData(3, new int[2, 2] { {1, 4}, {6, 7} }, new int[1, 2] { { 2, 5 } });
    }

    private void Start() {
        SaveData();
    }
}
