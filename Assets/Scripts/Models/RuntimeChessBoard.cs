public class RuntimeChessBoard
{
    private const int COLUMNS = 4;
    private const int ROWS = 8;
    private RuntimeChess[,] chessBoard = new RuntimeChess[COLUMNS, ROWS];

    private bool IsSlotEmpty(int x, int y) {
        if (x < 0 || x >= COLUMNS || y < 0 || y >= ROWS) return false;
        return chessBoard[x, y] == null;
    }

    public void PlaceChess(CardDataSO cardData, ChessSlotController slot) {
        int chessX = slot.slotX;
        int chessY = slot.slotY;
        
        RuntimeChess chess = new RuntimeChess(cardData, chessX, chessY);
        chessBoard[chessX, chessY] = chess;
        BattleManager.Instance.InstantiateChess(cardData.chessPrefab, slot.transform.position);
    }

    public bool IsSlotValid(int slotX, int slotY) {
        if (slotY >= 4) return false; // todo: 如果后续有可以下在敌方的棋子需要修改
        if (!IsSlotEmpty(slotX, slotY)) return false;
        return true;
    }
}
