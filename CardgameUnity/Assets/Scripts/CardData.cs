using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Game/Card")]
public class CardData : ScriptableObject
{
    public string cardName;
    public string description;
    public Sprite artwork;
    public int damage;
    public int cost;
    public bool affectsRow;
}
