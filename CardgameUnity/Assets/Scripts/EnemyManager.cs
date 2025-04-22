using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    public GameObject baseEnemyPrefab;
    public GameObject defenderPrefab;
    public Transform gridContainer;
    public Transform[,] gridCells = new Transform[4, 3];

    public List<Enemy> activeEnemies = new List<Enemy>();
    private Dictionary<(int row, int col), List<Enemy>> enemyStacks = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeGridFromContainer();
        SpawnTopRow();
    }

    public void SpawnTopRow()
    {
        bool spawnDefenders = GameManager.Instance.turnNumber % 3 == 0;

        for (int col = 0; col < 3; col++)
        {
            int row = 0;
            Transform cell = gridCells[row, col];

            if (!enemyStacks.ContainsKey((row, col)))
                enemyStacks[(row, col)] = new List<Enemy>();

            GameObject prefab = spawnDefenders ? defenderPrefab : baseEnemyPrefab;
            GameObject enemyObj = Instantiate(prefab, cell);
            Enemy enemy = enemyObj.GetComponent<Enemy>();

            enemy.row = row;
            enemy.col = col;
            enemy.Initialize();

            AddEnemyToStack(enemy, row, col);
            activeEnemies.Add(enemy);
        }
    }

    public void MoveEnemy(Enemy enemy, int newRow, int newCol)
    {
        if (enemyStacks.TryGetValue((enemy.row, enemy.col), out var oldStack))
        {
            oldStack.Remove(enemy);
        }

        AddEnemyToStack(enemy, newRow, newCol);

        enemy.row = newRow;
        enemy.col = newCol;
    }

    public void AddEnemyToStack(Enemy enemy, int row, int col)
    {
        var key = (row, col);
        if (!enemyStacks.ContainsKey(key))
            enemyStacks[key] = new List<Enemy>();

        var stack = enemyStacks[key];
        stack.Add(enemy);

        stack = stack
            .OrderByDescending(e => e.StackPriority)
            .ThenBy(e => e.SpawnOrder)
            .ToList();

        enemyStacks[key] = stack;

        ApplyStacking(row, col);
    }

    public void ApplyStacking(int row, int col)
    {
        var stack = enemyStacks[(row, col)];
        for (int i = 0; i < stack.Count; i++)
        {
            var enemy = stack[i];
            enemy.transform.SetParent(gridCells[row, col]);
            enemy.transform.localPosition = GetStackOffset(i);
            enemy.transform.SetSiblingIndex(stack.Count - 1 - i);
        }
    }

    public void ReorganizeAllStacks()
    {
        foreach (var key in enemyStacks.Keys.ToList())
        {
            var stack = enemyStacks[key]
                .OrderByDescending(e => e.StackPriority)
                .ThenBy(e => e.SpawnOrder)
                .ToList();

            enemyStacks[key] = stack;

            ApplyStacking(key.Item1, key.Item2);
        }
    }

    private Vector3 GetStackOffset(int index)
    {
        float offset = 10f;
        return new Vector3(index * offset, index * offset, 0f);
    }

    private void InitializeGridFromContainer()
    {
        if (gridContainer.childCount < 12)
        {
            Debug.LogError("GridContainer must have 12 children (4 rows x 3 cols)");
            return;
        }

        int index = 0;
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                gridCells[row, col] = gridContainer.GetChild(index);
                index++;
            }
        }
    }

    public void ProcessEnemies()
    {
        Debug.Log("Processing enemies...");

        foreach (Enemy enemy in activeEnemies.ToArray()) // Clone list to avoid mutation issues
        {
            if (enemy != null)
            {
                enemy.EndTurn(); // Each enemy either moves or attacks
            }
        }
    }

    public Enemy GetTopEnemyAt(int row, int col)
    {
        var key = (row, col);
        if (enemyStacks.TryGetValue(key, out var stack) && stack.Count > 0)
        {
            return stack[0]; // top of the stack is always index 0 after sorting
        }

        return null;
    }


}
