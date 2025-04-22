using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Card Effects/Damage Effect")]
public class DamageEffect : CardEffect
{
    public int damageAmount;

    public override void ApplyEffect(CardInstance card, Vector2Int targetPosition)
    {
        // Logic for damaging enemy at targetPosition
        Enemy target = EnemyManager.Instance.GetTopEnemyAt(targetPosition.x, targetPosition.y);
        if (target != null)
        {
            target.TakeDamage(damageAmount);
        }
    }
}
