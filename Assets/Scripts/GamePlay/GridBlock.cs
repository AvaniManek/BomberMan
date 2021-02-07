using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum BlockChildType
{
    Empty = 0,
    SolidBlock = 1,
    ExplodableBlock = 2,
    Character = 4,
    Bomb = 8,
    Enemy = 16,
    Fire = 32,
    EnemyAndCharacter = 20,
    FireAndCharacter = 36,
}

public enum GameState
{
    None = 0,
    Lost = 1,
    Won = 2,
    Started = 3
}

public class GridBlock : MonoBehaviour
{
    public delegate void GameStateHandler(GameState gameState);
    public static GameStateHandler gameOver;

    [SerializeField]
    private int _rowIndex;

    [SerializeField]
    private int _columnIndex;

    [SerializeField]
    private BlockChildType _child;

    public int RowIndex { get => _rowIndex; set => _rowIndex = value; }
    public int ColumnIndex { get => _columnIndex; set => _columnIndex = value; }
    public BlockChildType Child
    {
        get => _child;
        set
        {
            _child = value;
            if (GameManager.Instance.GameStatus == GameState.Started)
            {
                if ((_child & BlockChildType.Enemy) != 0 && (_child & BlockChildType.Character) != 0)
                {
                    DispatchGameOver(GameState.Lost);
                }

                if ((_child & BlockChildType.Fire) != 0 && (_child & BlockChildType.Character) != 0)
                {
                    DispatchGameOver(GameState.Lost);
                }
                CheckForEnemyKilling();
            }
        }
    }

    private void CheckForEnemyKilling()
    {
        if (LevelManager.Instance.IsAllEnemiesKilled())
        {
            Debug.Break();
            //DispatchGameOver(GameState.Won);
        }
    }

    private void DispatchGameOver(GameState gameState)
    {
        if (gameOver != null)
        {
            gameOver(gameState);
        }
    }
}
