using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PuzzleData : MonoBehaviour {
    public int puzzleSize;
    public List<PieceData> pieces = new List<PieceData>();
}
