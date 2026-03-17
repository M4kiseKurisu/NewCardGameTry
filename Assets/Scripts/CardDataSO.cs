using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "CardGame/CardData")]
public class CardDataSO : ScriptableObject
{
    public string cardID;
    public string cardName;
}
