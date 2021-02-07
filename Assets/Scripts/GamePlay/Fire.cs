using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private GridBlock _parentBlock;

    public GridBlock ParentBlock { get => _parentBlock; set => _parentBlock = value; }

    private void Start() 
    {
        Invoke("SelfDestruct", 1);
    }

    private void SelfDestruct() 
    {
        Transform[] _childObjects = ParentBlock.transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < _childObjects.Length; i++)
        {
            if (!_childObjects[i].CompareTag(Constants.SOLID_BLOCK_TAG))
            {
                Destroy(_childObjects[i].gameObject);
                ParentBlock.Child = BlockChildType.Empty;
            }
        }
    }
}
