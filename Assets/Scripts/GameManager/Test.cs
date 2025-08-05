using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
        public void SaveDataToJson(int size, int[,] horizontal, int[,] vertical) {
            // constructor 호출로 데이터 채움
            PuzzleInitData saveData = new PuzzleInitData(size, horizontal, vertical);

            // 디버깅: 데이터 확인
            //Debug.Log("puzzleSize: " + saveData.puzzleSize);
            //Debug.Log("horizontalSide 크기: " + saveData.horizontalSide.Count);
            //Debug.Log("verticalSide 크기: " + saveData.verticalSide.Count);

            string jsonData = JsonUtility.ToJson(saveData);
            Debug.Log(jsonData);
            string path = Path.Combine(Application.persistentDataPath, "puzzleInit.json");
            File.WriteAllText(path, jsonData);
            Debug.Log("저장된 JSON: " + jsonData);
        }

        private void Start() {
            int[,] a = new int[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            SaveDataToJson(3, a, a);
        }
    
}
