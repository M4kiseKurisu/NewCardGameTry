using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using TMPro;

public class CardController : MonoBehaviour
{
    private const int MAX_SORTING_LAYER = 999;
    private const int MAX_SORTING_LAYER_WHEN_DRAGGING = 9999;
    public CardDataSO cardData;
    public bool isDragging{ get; private set; }
    
    private Transform visualChildTransform;
    private SortingGroup sortingGroup;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private int originalSortingOrder;
    private Vector3 cardDragOffset;
    private HandOfCardController handController;
    private ChessSlotController currentHoveredSlot;
    

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
        
        SetCardTransparency(UIConfigSO.Instance.CardDraggingOnChessBoardAlpha);
    }

    protected void OnMouseDrag() {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPosition.x + cardDragOffset.x, 
            mouseWorldPosition.y + cardDragOffset.y, transform.position.z);
        
        Collider2D hitCollider = Physics2D.OverlapPoint(mouseWorldPosition, LayerMask.GetMask("BoardSlot"));
        if (hitCollider != null && hitCollider.TryGetComponent(out ChessSlotController chessSlot)) {
            if (currentHoveredSlot != chessSlot) {
                if (currentHoveredSlot != null) currentHoveredSlot.SetHighlight(false);
                currentHoveredSlot = chessSlot;
                if (currentHoveredSlot.IsSlotValid()) currentHoveredSlot.SetHighlight(true);
            }
        }
        else {
            if (currentHoveredSlot != null) {
                currentHoveredSlot.SetHighlight(false);
                currentHoveredSlot = null;
            }
        }
    }

    protected void OnMouseUp() {
        isDragging = false;
        visualChildTransform.DOScale(originalScale, UIConfigSO.Instance.MouseDraggingRecoverAnimationDuration);
        
        SetCardTransparency(1f);
        if (currentHoveredSlot != null && currentHoveredSlot.IsSlotValid()) {
            currentHoveredSlot.SetHighlight(false);
            // todo: 卡牌出牌后的结算逻辑
            handController.RemoveCard(gameObject);
            Destroy(gameObject);
        }
        else 
            handController.UpdateCardLayout();
    }
    
    public void SetBaseSortingOrder(int order) {
        sortingGroup.sortingOrder = order;
        originalSortingOrder = order;
    }

    private void SetCardTransparency(float alpha) {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in sprites) {
            Color newColor = sr.color;
            newColor.a = alpha;
            sr.color = newColor;
        }
        
        TextMeshPro[] texts = GetComponentsInChildren<TextMeshPro>();
        foreach (var text in texts) {
            Color newColor = text.color;
            newColor.a = alpha;
            text.color = newColor;
        }
    }
}
