using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class BoardGenerator : MonoBehaviour
    {
        [SerializeField] bool _generateInEditor;
        [SerializeField] GameObject _tilePrefab;
        [SerializeField] int _boardSizeX = 10, _boardSizeZ = 10;

        List<GameObject> _tileList = new List<GameObject>();

        #region GENERATE_IN_EDITOR
        private void OnDrawGizmos()
        {
            if (_tilePrefab != null && _generateInEditor)
            {
                DeleteExistingTiles();
                GenerateBoard();
            }
        }

        void DeleteExistingTiles()
        {
            for (int i = 0; i < _tileList.Count; i++)
                DestroyImmediate(_tileList[i]);

            _tileList.Clear();
        }
        #endregion

        private void Start()
        {
            GenerateBoard();
        }

        void GenerateBoard()
        {
            for (int i = 0; i < _boardSizeX; i++)
            {
                for (int j = 0; j < _boardSizeZ; j++)
                {
                    Vector3 tilePos = new Vector3(transform.position.x + i, 0, transform.position.z + j);
                    GameObject tile = Instantiate(_tilePrefab, tilePos, Quaternion.identity, transform);
                    tile.name = $"Tile: x {i}, z {j}";

                    _tileList.Add(tile); 
                }
            }
        }
    }
}
