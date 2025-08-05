using System.Collections.Generic;



[System.Serializable]
public class PuzzleInitData {
    public int puzzleSize;
    public List<List<int>> horizontalSide = new List<List<int>>();
    public List<List<int>> verticalSide = new List<List<int>>();

    public PuzzleInitData(int size, int[,] horizontal, int[,] vertical) {
        puzzleSize = size;
        for (int i = 0; i < horizontal.GetLength(0); i++) {
            List<int> row = new List<int>();
            for (int j = 0; j < horizontal.GetLength(1); j++) {
                row.Add(horizontal[i, j]);
            }
            horizontalSide.Add(row);
        }

        for (int i = 0; i < vertical.GetLength(0); i++) {
            List<int> row = new List<int>();
            for (int j = 0; j < vertical.GetLength(1); j++) {
                row.Add(vertical[i, j]);
            }
            verticalSide.Add(row);
        }
    }
}
