using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/Card Data")]
public class CardData : ScriptableObject
{
    public string cardName;
    public int cost;
    public Sprite artwork;
    public CardEffect effect;
}

