using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject _firePrefab;

    private List<Fire> _fireObjects = new List<Fire>();
    [SerializeField]
    private GridBlock _parentBlock;
    private int _coverageGridSize = 4;

    public GridBlock ParentBlock { get => _parentBlock; set => _parentBlock = value; }

    public void Blast() 
    {
        for (int i = 0; i < _coverageGridSize; i++)
        {
            if (((ParentBlock.RowIndex - i) >= 0) && ((ParentBlock.RowIndex - i) <= (Constants.maxNoOfRows - 1)))
            {
                GridBlock _fireParentBlock = LevelManager.Instance.GridBlocks.Find(gb => gb.RowIndex == ParentBlock.RowIndex - i && gb.ColumnIndex == ParentBlock.ColumnIndex);
                Fire _fire = Instantiate(_firePrefab, _fireParentBlock.transform).GetComponent<Fire>();
                _fireObjects.Add(_fire);
                _fireParentBlock.Child |= BlockChildType.Fire;
                _fire.ParentBlock = _fireParentBlock;
            }

            if (((ParentBlock.RowIndex + i) >= 0) && ((ParentBlock.RowIndex + i) <= (Constants.maxNoOfRows - 1)))
            {
                GridBlock _fireParentBlock = LevelManager.Instance.GridBlocks.Find(gb => gb.RowIndex == ParentBlock.RowIndex + i && gb.ColumnIndex == ParentBlock.ColumnIndex);
                Fire _fire = Instantiate(_firePrefab, _fireParentBlock.transform).GetComponent<Fire>();
                _fireObjects.Add(_fire);
                _fireParentBlock.Child |= BlockChildType.Fire;
                _fire.ParentBlock = _fireParentBlock;
            }

            if (((ParentBlock.ColumnIndex - i) >= 0) && ((ParentBlock.ColumnIndex - i) <= (Constants.maxNoOfColumns - 1)))
            {
                GridBlock _fireParentBlock = LevelManager.Instance.GridBlocks.Find(gb => gb.RowIndex == ParentBlock.RowIndex && gb.ColumnIndex == ParentBlock.ColumnIndex - i);
                Fire _fire = Instantiate(_firePrefab, _fireParentBlock.transform).GetComponent<Fire>();
                _fireObjects.Add(_fire);
                _fireParentBlock.Child |= BlockChildType.Fire;
                _fire.ParentBlock = _fireParentBlock;
            }

            if (((ParentBlock.RowIndex + i) >= 0) && ((ParentBlock.ColumnIndex + i) <= (Constants.maxNoOfColumns - 1)))
            {
                GridBlock _fireParentBlock = LevelManager.Instance.GridBlocks.Find(gb => gb.RowIndex == ParentBlock.RowIndex && gb.ColumnIndex == ParentBlock.ColumnIndex + i);
                Fire _fire = Instantiate(_firePrefab, _fireParentBlock.transform).GetComponent<Fire>();
                _fireObjects.Add(_fire);
                _fireParentBlock.Child |= BlockChildType.Fire;
                _fire.ParentBlock = _fireParentBlock;
            }
        }
    }
}
