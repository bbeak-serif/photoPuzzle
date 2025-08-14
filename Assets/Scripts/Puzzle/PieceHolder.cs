using UnityEngine;
using UnityEngine.EventSystems;

public class PieceHolder : MonoBehaviour, IDropHandler
{
    public Vector2Int CurrentPos;

    public void OnDrop(PointerEventData eventData) {
        PuzzleDragAndDrop draggableItem = eventData.pointerDrag.GetComponent<PuzzleDragAndDrop>();
        if(draggableItem.CorrectPos == CurrentPos) {
            draggableItem.transform.SetParent(this.transform);
            draggableItem.isCorrect = true;
            draggableItem.Fiting();
            FindFirstObjectByType<PuzzleManager>().UpdateBoard(CurrentPos);
        }
    }
}
