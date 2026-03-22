using UnityEngine;

public class ChessSlotController : MonoBehaviour
{
    public int slotX;
    public int slotY;
    private GameObject highlightObject;

    private void Awake() {
        highlightObject = transform.GetChild(0).gameObject;
        highlightObject.SetActive(false);
    }

    public bool IsSlotValid() {
        return BattleManager.Instance.chessBoard.IsSlotValid(slotX, slotY);
    }

    public void SetHighlight(bool show) {
        highlightObject.SetActive(show);
    }
}
