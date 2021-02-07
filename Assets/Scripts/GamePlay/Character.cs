using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private GridBlock _parentBlock;

    [SerializeField]
    private GameObject _bombPrefab;

    private Bomb _bomb;
    private float movingTime = 0.1f;
    private float _waitTimeBeforeBlast = 3f;
    private RectTransform _rectTransform;
    private bool isRunning;

    public GridBlock ParentBlock { get => _parentBlock; set => _parentBlock = value; }
    public Bomb Bomb { get => _bomb; }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!LevelManager.Instance.IsAnyBlockHasBomb())
            {
                PlantBomb();
            }
        }
    }

    private void Start() 
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void PlantBomb() 
    {
        _bomb = Instantiate(_bombPrefab, ParentBlock.transform).GetComponent<Bomb>();
        ParentBlock.Child |= BlockChildType.Bomb;
        _bomb.ParentBlock = ParentBlock;
        Invoke("BlastBomb", _waitTimeBeforeBlast);
    }

    private void BlastBomb()
    {
        _bomb.Blast();
    }

    IEnumerator MoveToTarget(Vector2 targetPosition, float time) 
    {
        isRunning = true;
        Vector2 currentPosition = _rectTransform.anchoredPosition;
        float fraction = 0;
        float rate = 1 / time;

        while (fraction <= 1)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(currentPosition, targetPosition, fraction);
            fraction += Time.deltaTime * rate;
            yield return 0;
        }
        isRunning = false;
    }

    private void MoveRight() 
    {
        if (ParentBlock.ColumnIndex < (Constants.maxNoOfColumns - 1))
        {
            GridBlock _nextBlock = LevelManager.Instance.GridBlocks.Find(gb => gb.RowIndex == ParentBlock.RowIndex && gb.ColumnIndex == ParentBlock.ColumnIndex + 1);
            if (_nextBlock.Child == BlockChildType.Empty || _nextBlock.Child == BlockChildType.Fire || _nextBlock.Child == BlockChildType.Enemy)
            {
                transform.SetParent(_nextBlock.transform);
                if (!isRunning)
                {
                    StartCoroutine(MoveToTarget(Vector2.zero, movingTime));
                }
                ParentBlock.Child &= ~(BlockChildType.Character);
                _nextBlock.Child |= BlockChildType.Character;
                ParentBlock = _nextBlock;
            }
        }
    }

    private void MoveLeft() 
    {
        if (ParentBlock.ColumnIndex > 0)
        {
            GridBlock _nextBlock = LevelManager.Instance.GridBlocks.Find(gb => gb.RowIndex == ParentBlock.RowIndex && gb.ColumnIndex == ParentBlock.ColumnIndex - 1);
            if (_nextBlock.Child == BlockChildType.Empty || _nextBlock.Child == BlockChildType.Fire || _nextBlock.Child == BlockChildType.Enemy)
            {
                transform.SetParent(_nextBlock.transform);
                if (!isRunning)
                {
                    StartCoroutine(MoveToTarget(Vector2.zero, movingTime));
                }
                ParentBlock.Child &= ~(BlockChildType.Character);
                _nextBlock.Child |= BlockChildType.Character;
                ParentBlock = _nextBlock;
            }
        }
    }

    private void MoveDown() 
    {
        if (ParentBlock.RowIndex < (Constants.maxNoOfRows - 1))
        {
            GridBlock _nextBlock = LevelManager.Instance.GridBlocks.Find(gb => gb.RowIndex == ParentBlock.RowIndex + 1 && gb.ColumnIndex == ParentBlock.ColumnIndex);
            if (_nextBlock.Child == BlockChildType.Empty || _nextBlock.Child == BlockChildType.Fire || _nextBlock.Child == BlockChildType.Enemy)
            {
                transform.SetParent(_nextBlock.transform);
                if (!isRunning)
                {
                    StartCoroutine(MoveToTarget(Vector2.zero, movingTime));
                }
                ParentBlock.Child &= ~(BlockChildType.Character);
                _nextBlock.Child |= BlockChildType.Character;
                ParentBlock = _nextBlock;
            }
        }
    }

    private void MoveUp() 
    {
        if (ParentBlock.RowIndex > 0)
        {
            GridBlock _nextBlock = LevelManager.Instance.GridBlocks.Find(gb => gb.RowIndex == ParentBlock.RowIndex - 1 && gb.ColumnIndex == ParentBlock.ColumnIndex);
            if (_nextBlock.Child == BlockChildType.Empty || _nextBlock.Child == BlockChildType.Fire || _nextBlock.Child == BlockChildType.Enemy)
            {
                transform.SetParent(_nextBlock.transform);
                if (!isRunning)
                {
                    StartCoroutine(MoveToTarget(Vector2.zero, movingTime));
                }
                ParentBlock.Child &= ~(BlockChildType.Character);
                _nextBlock.Child |= BlockChildType.Character;
                ParentBlock = _nextBlock;
            }
        }

    }
}
