using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    [SerializeField]
    private List<GridBlock> _gridBlocks;

    [SerializeField]
    private GameObject _explodableBlockPrefab;

    [SerializeField]
    private GameObject _characterPrefab;

    [SerializeField]
    private GameObject _enemyPrefab;

    private int _noOfEnemies = 5;
    public int noOfExplodableBlocks;
    private Character _character;

    public static LevelManager Instance { get => _instance; }
    public List<GridBlock> GridBlocks { get => _gridBlocks; }
    public Character Character { get => _character; }

    private void Awake() 
    {
        _instance = this;
    }

    private void Start() 
    {
        GenerateLevel();
    }

    private void GenerateLevel() 
    {
        GridBlock _randomBlock = GetRandomEmptyBlock();
        _character = Instantiate(_characterPrefab, _randomBlock.transform).GetComponent<Character>();
        _randomBlock.Child |= BlockChildType.Character;
        Character.ParentBlock = _randomBlock;

        for (int i = 0; i < noOfExplodableBlocks; i++)
        {
            _randomBlock = GetRandomEmptyBlock();
            while ((_randomBlock.ColumnIndex == Character.ParentBlock.ColumnIndex - 1) || (_randomBlock.ColumnIndex == Character.ParentBlock.ColumnIndex + 1))
            {
                _randomBlock = GetRandomEmptyBlock();
            }
            Instantiate(_explodableBlockPrefab, _randomBlock.transform);
            _randomBlock.Child |= BlockChildType.ExplodableBlock;
        }

        for (int i = 0; i < _noOfEnemies; i++)
        {
            _randomBlock = GetRandomEmptyBlock();
            while ((_randomBlock.ColumnIndex == Character.ParentBlock.ColumnIndex - 1) || (_randomBlock.ColumnIndex == Character.ParentBlock.ColumnIndex + 1))
            {
                _randomBlock = GetRandomEmptyBlock();
            }
            Enemy _enemy = Instantiate(_enemyPrefab, _randomBlock.transform).GetComponent<Enemy>();
            _randomBlock.Child |= BlockChildType.Enemy;
            _enemy.ParentBlock = _randomBlock;
        }

        GameManager.Instance.GameStatus = GameState.Started;
    }

    private GridBlock GetRandomEmptyBlock() 
    {
        int randomGridBlockIndex = Random.Range(0, GridBlocks.Count - 1);
        while (GridBlocks[randomGridBlockIndex].Child != BlockChildType.Empty)
        {
            randomGridBlockIndex = Random.Range(0, GridBlocks.Count - 1);
        }
        return GridBlocks[randomGridBlockIndex];
    }

    public bool IsAllEnemiesKilled() 
    {
        for (int i = 0; i < _gridBlocks.Count; i++)
        {
            if ((_gridBlocks[i].Child & BlockChildType.Enemy) != 0)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsAnyBlockHasBomb() 
    {
        for (int i = 0; i < _gridBlocks.Count; i++)
        {
            if ((_gridBlocks[i].Child & BlockChildType.Bomb) != 0)
            {
                return true;
            }
        }
        return false;
    }
}
