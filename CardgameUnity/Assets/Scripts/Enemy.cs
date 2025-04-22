using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int Attack = 1;
    public int HitPoints = 3;
    public int Level = 1;

    public int row, col;
    public int SpawnOrder { get; set; }
    private static int GlobalSpawnCounter = 0;

    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI healthText;

    public virtual bool IsDefender => false;
    public virtual int StackPriority => 0;

    protected virtual void Awake()
    {
        Attack = 1;
        HitPoints = 3;
        Level = 1;
    }

    public virtual void Initialize()
    {
        SpawnOrder = GlobalSpawnCounter++;
        UpdateDisplay();
    }

    protected void UpdateDisplay()
    {
        if (attackText != null) attackText.text = $"A{Attack}";
        if (healthText != null) healthText.text = $"HP{HitPoints}";
    }

    public void EndTurn()
    {
        if (row == 3)
        {
            PerformAttack();
        }
        else
        {
            Move();
        }
    }

    public void Move()
    {
        if (row < 3)
        {
            EnemyManager.Instance.MoveEnemy(this, row + 1, col);
        }
    }

    public void PerformAttack()
    {
        GameManager.Instance.TakeDamage(Attack);
    }

    public void TakeDamage(int damageTaken)
    {
        HitPoints -= damageTaken;
        if (HitPoints < 1)
            Debug.Log("This enemy is dead.");
        UpdateDisplay();
        // TODO: Remove from stack and destroy
    }
}
