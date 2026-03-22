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
        // todo: 判定当前格子是否可以放对应chess
        return true;
    }

    public void SetHighlight(bool show) {
        highlightObject.SetActive(show);
    }
}
