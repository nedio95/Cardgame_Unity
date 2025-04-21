using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int playerHealth = 10;
    public int turnNumber = 1;
    public TextMeshProUGUI healthText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateHealthUI();
    }

    public void EndTurn()
    {
        turnNumber++;
        EnemyManager.Instance.ProcessEnemies();
        EnemyManager.Instance.SpawnTopRow();
        EnemyManager.Instance.ReorganizeAllStacks();
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"HP: {playerHealth}";
        }
    }


}
