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
}
