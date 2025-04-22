using UnityEngine;

public abstract class CardEffect : ScriptableObject
{
    public string effectName;
    public string description;

    // This is the key method every effect must implement
    public abstract void ApplyEffect(CardInstance card, Vector2Int targetPosition);
}
