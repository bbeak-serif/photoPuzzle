using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
        public void SaveDataToJson(int size, int[,] horizontal, int[,] vertical) {
            // constructor ȣ��� ������ ä��
            PuzzleInitData saveData = new PuzzleInitData(size, horizontal, vertical);

            // �����: ������ Ȯ��
            //Debug.Log("puzzleSize: " + saveData.puzzleSize);
            //Debug.Log("horizontalSide ũ��: " + saveData.horizontalSide.Count);
            //Debug.Log("verticalSide ũ��: " + saveData.verticalSide.Count);

            string jsonData = JsonUtility.ToJson(saveData);
            Debug.Log(jsonData);
            string path = Path.Combine(Application.persistentDataPath, "puzzleInit.json");
            File.WriteAllText(path, jsonData);
            Debug.Log("����� JSON: " + jsonData);
        }

        private void Start() {
            int[,] a = new int[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            SaveDataToJson(3, a, a);
        }
    
}
