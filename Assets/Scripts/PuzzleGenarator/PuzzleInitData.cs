using System.Collections.Generic;
using System;

[System.Serializable]
public class PuzzleInitData {
    public int puzzleSize;
    public int[] flattenHoriziontalSide;
    public int[] flattenVerticalSide;

    public PuzzleInitData(int size, int[,] horizontalSide, int[,] verticalSide) {
        puzzleSize = size;

        flattenHoriziontalSide = new int[(size + 1) * size];
        flattenVerticalSide = new int[size * (size + 1)];
        int i = 0;
        foreach(int inner in horizontalSide) {
            flattenHoriziontalSide[i] = inner;
            i++;
        }

        i = 0;
        foreach(int inner in verticalSide) {
            flattenVerticalSide[i] = inner;
            i++;
        }
    }

    public int[,] RestoreHorizontalData() {
        int[,] returns = new int[puzzleSize + 1, puzzleSize];
        int index = 0;

        for (int i = 0; i <= puzzleSize; i++) {
            for (int j = 0; j < puzzleSize; j++) {
                returns[i, j] = flattenHoriziontalSide[index];
                index++;
            }
        }
        return returns;
    }

    public int[,] RestoreVerticalData() {
        int[,] returns = new int[puzzleSize, puzzleSize + 1];
        int index = 0;

        for (int i = 0; i < puzzleSize; i++) {
            for (int j = 0; j <= puzzleSize; j++) {
                returns[i, j] = flattenVerticalSide[index];
                index++;
            }
        }
        return returns;
    }
}
