using System;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class CardController : MonoBehaviour
{
    private const int MAX_SORTING_LAYER = 999;
    private const int MAX_SORTING_LAYER_WHEN_DRAGGING = 9999;
    public bool isDragging{ get; private set; }
    
    private Transform visualChildTransform;
    private SortingGroup sortingGroup;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private int originalSortingOrder;
    private Vector3 cardDragOffset;
    private HandOfCardController handController;
    

    protected void Awake() {
        visualChildTransform = transform.GetChild(0);
        sortingGroup = visualChildTransform.GetComponent<SortingGroup>();
        originalPosition = visualChildTransform.localPosition;
        originalScale = visualChildTransform.localScale;
        originalSortingOrder = sortingGroup.sortingOrder;
    }

    protected void Start() {
        handController = FindFirstObjectByType<HandOfCardController>();
    }

    protected void OnMouseEnter() {
        if (isDragging) return;
        sortingGroup.sortingOrder = MAX_SORTING_LAYER;
        visualChildTransform.DOKill();
        visualChildTransform.DOLocalMoveY(originalPosition.y + UIConfigSO.Instance.MouseHoverPopYOffset,
            UIConfigSO.Instance.MouseHoverPopAnimationDuration);
        visualChildTransform.DOScale(originalScale * UIConfigSO.Instance.MouseHoverZoomInFactor, 
            UIConfigSO.Instance.MouseHoverPopAnimationDuration);
    }

    protected void OnMouseExit() {
        if (isDragging) return;
        sortingGroup.sortingOrder = originalSortingOrder;
        visualChildTransform.DOKill();
        visualChildTransform.DOLocalMove(originalPosition, UIConfigSO.Instance.MouseHoverRecoverAnimationDuration);
        visualChildTransform.DOScale(originalScale, UIConfigSO.Instance.MouseHoverRecoverAnimationDuration);
    }

    protected void OnMouseDown() {
        isDragging = true;
        sortingGroup.sortingOrder = MAX_SORTING_LAYER_WHEN_DRAGGING;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cardDragOffset = transform.position - new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
        
        visualChildTransform.DOScale(originalScale * UIConfigSO.Instance.MouseDraggingZoomInFactor,
            UIConfigSO.Instance.MouseDraggingZoomInAnimationDuration);
    }

    protected void OnMouseDrag() {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPosition.x + cardDragOffset.x, 
            mouseWorldPosition.y + cardDragOffset.y, transform.position.z);
    }

    protected void OnMouseUp() {
        isDragging = false;
        visualChildTransform.DOScale(originalScale, UIConfigSO.Instance.MouseDraggingRecoverAnimationDuration);
        handController.UpdateCardLayout();
    }
    
    public void SetBaseSortingOrder(int order) {
        sortingGroup.sortingOrder = order;
        originalSortingOrder = order;
    }
}
