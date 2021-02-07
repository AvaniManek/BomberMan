using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GridBlock _parentBlock;

    public GridBlock ParentBlock { get => _parentBlock; set => _parentBlock = value; }

    private RectTransform _rectTransform;
    private bool isRunning;
    private float movingTime = 1;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        StartCoroutine(MoveTowardsCharacter());
    }

    private IEnumerator MoveTowardsCharacter() 
    {
        while (true)
        {
            if (LevelManager.Instance.Character.ParentBlock.ColumnIndex > ParentBlock.ColumnIndex)
            {
                MoveRight();
            }
            if (LevelManager.Instance.Character.ParentBlock.ColumnIndex < ParentBlock.ColumnIndex)
            {
                MoveLeft();
            }

            if (LevelManager.Instance.Character.ParentBlock.RowIndex > ParentBlock.RowIndex)
            {
                MoveDown();
            }
            if (LevelManager.Instance.Character.ParentBlock.RowIndex < ParentBlock.RowIndex)
            {
                MoveUp();
            }
            yield return new WaitForSeconds(movingTime);
        }
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
            if (_nextBlock.Child == BlockChildType.Empty || _nextBlock.Child == BlockChildType.Fire || _nextBlock.Child == BlockChildType.Character)
            {
                transform.SetParent(_nextBlock.transform);
                if (!isRunning)
                {
                    StartCoroutine(MoveToTarget(Vector2.zero, movingTime));
                }
                ParentBlock.Child &= ~(BlockChildType.Enemy);
                _nextBlock.Child |= BlockChildType.Enemy;
                ParentBlock = _nextBlock;
            }
            else if (_nextBlock.Child == BlockChildType.SolidBlock)
            {
                MoveDown();
            }
        }
    }

    private void MoveLeft()
    {
        if (ParentBlock.ColumnIndex > 0)
        {
            GridBlock _nextBlock = LevelManager.Instance.GridBlocks.Find(gb => gb.RowIndex == ParentBlock.RowIndex && gb.ColumnIndex == ParentBlock.ColumnIndex - 1);
            if (_nextBlock.Child == BlockChildType.Empty || _nextBlock.Child == BlockChildType.Fire || _nextBlock.Child == BlockChildType.Character)
            {
                transform.SetParent(_nextBlock.transform);
                if (!isRunning)
                {
                    StartCoroutine(MoveToTarget(Vector2.zero, movingTime));
                }
                ParentBlock.Child &= ~(BlockChildType.Enemy);
                _nextBlock.Child |= BlockChildType.Enemy;
                ParentBlock = _nextBlock;
            }
            else if (_nextBlock.Child == BlockChildType.SolidBlock)
            {
                MoveUp();
            }
        }
    }

    private void MoveDown()
    {
        if (ParentBlock.RowIndex < (Constants.maxNoOfRows - 1))
        {
            GridBlock _nextBlock = LevelManager.Instance.GridBlocks.Find(gb => gb.RowIndex == ParentBlock.RowIndex + 1 && gb.ColumnIndex == ParentBlock.ColumnIndex);
            if (_nextBlock.Child == BlockChildType.Empty || _nextBlock.Child == BlockChildType.Fire || _nextBlock.Child == BlockChildType.Character)
            {
                transform.SetParent(_nextBlock.transform);
                if (!isRunning)
                {
                    StartCoroutine(MoveToTarget(Vector2.zero, movingTime));
                }
                ParentBlock.Child &= ~(BlockChildType.Enemy);
                _nextBlock.Child |= BlockChildType.Enemy;
                ParentBlock = _nextBlock;
            }
            else if (_nextBlock.Child == BlockChildType.SolidBlock)
            {
                MoveRight();
            }
        }
    }

    private void MoveUp()
    {
        if (ParentBlock.RowIndex > 0)
        {
            GridBlock _nextBlock = LevelManager.Instance.GridBlocks.Find(gb => gb.RowIndex == ParentBlock.RowIndex - 1 && gb.ColumnIndex == ParentBlock.ColumnIndex);
            if (_nextBlock.Child == BlockChildType.Empty || _nextBlock.Child == BlockChildType.Fire || _nextBlock.Child == BlockChildType.Character)
            {
                transform.SetParent(_nextBlock.transform);
                if (!isRunning)
                {
                    StartCoroutine(MoveToTarget(Vector2.zero, movingTime));
                }
                ParentBlock.Child &= ~(BlockChildType.Enemy);
                _nextBlock.Child |= BlockChildType.Enemy;
                ParentBlock = _nextBlock;
            }
            else if (_nextBlock.Child == BlockChildType.SolidBlock)
            {
                MoveLeft();
            }
        }

    }

}
