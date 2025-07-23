using UnityEngine;

[System.Serializable]
public class PieceData : MonoBehaviour
{
    [HideInInspector] public Vector2 CorrectPosition;
    [HideInInspector] public Vector2 Currentposition;
    [HideInInspector] public int bottomType, leftType, topType, rightType;
}
