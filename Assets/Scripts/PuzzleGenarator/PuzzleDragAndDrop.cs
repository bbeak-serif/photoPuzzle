using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
public class PuzzleDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Transform parentToReturnTo = null;
    public CanvasGroup canvasGroup;
    private Vector3 originalScale;
    public Vector2 CorrectPos;
    public bool isCorrect = false;

    private Canvas myCanvas;

    void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        myCanvas = GetComponent<Canvas>();
        myCanvas.overrideSorting = false;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (isCorrect) return;
        myCanvas.overrideSorting = true;
        myCanvas.sortingOrder = 99;

        parentToReturnTo = transform.parent;
        originalScale = transform.localScale;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        transform.localScale = originalScale * 2.5f;
    }
    public void OnDrag(PointerEventData eventData) {

        if (isCorrect) return;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (isCorrect) return;

        myCanvas.sortingOrder = 0;
        myCanvas.overrideSorting = false;
        transform.localScale = originalScale;
        if (transform.parent == transform.root)
        {
            transform.SetParent(parentToReturnTo);
            transform.localPosition = Vector3.zero;
        }

        canvasGroup.blocksRaycasts = true;
    }

    public void Fiting() {
        transform.localPosition = new Vector3(0, 0);
        transform.localScale = Vector3.one;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        myCanvas.overrideSorting = true;
        myCanvas.sortingOrder = 99;
    }
}
