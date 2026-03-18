using System;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class CardController : MonoBehaviour
{
    private const int MAX_SORTING_LAYER = 999;
    
    private Transform visualChildTransform;
    private SortingGroup sortingGroup;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private int originalSortingOrder;
    
    //public bool isDragging{ get; private set; }

    protected void Awake() {
        visualChildTransform = transform.GetChild(0);
        sortingGroup = visualChildTransform.GetComponent<SortingGroup>();
        originalPosition = visualChildTransform.localPosition;
        originalScale = visualChildTransform.localScale;
        originalSortingOrder = sortingGroup.sortingOrder;
    }

    protected void OnMouseEnter() {
        //if (isDragging) return;
        Debug.Log("Mouse Enter");
        sortingGroup.sortingOrder = MAX_SORTING_LAYER;
        visualChildTransform.DOKill();
        visualChildTransform.DOLocalMoveY(originalPosition.y + UIConfigSO.Instance.MouseHoverPopYOffset,
            UIConfigSO.Instance.MouseHoverPopAnimationDuration);
        visualChildTransform.DOScale(originalScale * UIConfigSO.Instance.MouseHoverZoomInFactor, 
            UIConfigSO.Instance.MouseHoverPopAnimationDuration);
    }

    protected void OnMouseExit() {
        //if (isDragging) return;
        sortingGroup.sortingOrder = originalSortingOrder;
        visualChildTransform.DOKill();
        visualChildTransform.DOLocalMove(originalPosition, UIConfigSO.Instance.MouseHoverRecoverAnimationDuration);
        visualChildTransform.DOScale(originalScale, UIConfigSO.Instance.MouseHoverRecoverAnimationDuration);
    }
}
