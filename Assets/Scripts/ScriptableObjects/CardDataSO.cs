using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "CardGame/CardData")]
public class CardDataSO : ScriptableObject
{
    public string cardID;
    public string cardName;
    public int basePower;
    public GameObject chessPrefab;
}
