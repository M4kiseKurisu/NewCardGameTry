using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandOfCardController : MonoBehaviour
{
    //todo: 将此处手动确认的prefab后续改成从外部引入prefab
    public GameObject exampleCardPrefab;

    private float cardWidth;
    private float cardSpacing;
    private float cardHeightOffset;
    private Transform initializeCardTransform;
    private List<GameObject> cardsInHand = new List<GameObject>();

    private void Start() {
        cardWidth = exampleCardPrefab.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        cardSpacing = UIConfigSO.Instance.CardLayoutSpacing;
        cardHeightOffset = UIConfigSO.Instance.CardLayoutHeightOffset;
        initializeCardTransform = GetComponentInChildren<Transform>();
    }

    private void Update() {
        // todo: 改成正确的发牌逻辑
        if (Input.GetKeyDown(KeyCode.Space))
            DrawCard();
    }

    private void DrawCard() {
        GameObject newCard = Instantiate(exampleCardPrefab, transform);
        newCard.transform.localPosition = initializeCardTransform.localPosition;
        cardsInHand.Add(newCard);
        UpdateCardLayout();
    }

    public void UpdateCardLayout() {
        int cardCount = cardsInHand.Count;
        if (cardCount <= 0) return;
        
        float totalHandOfCardWidth = (cardCount * cardWidth) + ((cardCount - 1) * cardSpacing);
        float startCardPositionX = -totalHandOfCardWidth / 2f + cardWidth / 2f;

        for (int i = 0; i < cardCount; i++) {
            float targetPositionX = startCardPositionX + i * (cardWidth + cardSpacing);
            float targetPositionY = CalculateCardPositionY(cardCount, i);
            
            CardController card = cardsInHand[i].GetComponent<CardController>();
            card.SetBaseSortingOrder(i);
            if (card.isDragging) continue;
            
            cardsInHand[i].transform.DOLocalMove(new Vector3(targetPositionX, targetPositionY, 0f), 
                UIConfigSO.Instance.CardLayoutAnimationDuration).SetEase(Ease.OutBack);
        }
    }

    public void RemoveCard(GameObject card) {
        if (cardsInHand.Contains(card)) {
            cardsInHand.Remove(card);
            UpdateCardLayout();
        }
    }

    private float CalculateCardPositionY(int totalCardCount, int cardIndex) {
        if (totalCardCount <= 1) return 0;
        float t = ((float)cardIndex / (totalCardCount - 1)) * 2f - 1f;
        float targetY = -(t * t) * cardHeightOffset;
        return targetY;
    }
}
