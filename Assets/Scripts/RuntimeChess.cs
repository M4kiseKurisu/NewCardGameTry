public class RuntimeChess
{
    public string instanceChessID { get; private set; }
    public CardDataSO originCardData { get; private set; }

    public int positionX;
    public int positionY;

    public RuntimeChess(CardDataSO cardData, int originalPositionX, int originalPositionY) {
        originCardData = cardData;
        positionX = originalPositionX;
        positionY = originalPositionY;
    }
}
