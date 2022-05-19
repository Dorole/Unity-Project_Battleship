using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class ShipPlacer : MonoBehaviour
    {
        [SerializeField] GameObject _board;
        [SerializeField] List<Ship> _shipsToPlace = new List<Ship>();
        int _currentShip;
        SO_ShipData _currentShipData;
        int _xRange;
        int _zRange;
        List<GameObject> _spawnedShips = new List<GameObject>(); //DEBUG

        TileInfo[,] _tempGrid = new TileInfo[10, 10];

        private void Start()
        {
            _xRange = _board.GetComponent<BoardGenerator>().BoardSizeX;
            _zRange = _board.GetComponent<BoardGenerator>().BoardSizeZ;

            ResetGrid();
        }

        #region TEST AREA
        public void TestPlacingPA()
        {
            bool posFound = false;

            for (int i = 0; i < _shipsToPlace.Count; i++)
            {
                int sameShipTypePlaced = 0;

                _currentShip = i;
                _currentShipData = _shipsToPlace[_currentShip].ShipData;

                for (int j = 0; j < _currentShipData.AmountToPlace; j++)
                {
                    if (sameShipTypePlaced == _currentShipData.AmountToPlace) return;

                    posFound = false;

                    while (!posFound)
                    {
                        int xPos = Random.Range(0, _xRange);
                        int zPos = Random.Range(0, _zRange);

                        GameObject pa = Instantiate(_currentShipData.PlacingAssisstant);
                        pa.transform.position = new Vector3(_board.transform.position.x + xPos, 0.6f, _board.transform.position.z + zPos);

                        //CHECK PREFABS - SOMETHING WRONG WITH ROTATIONS!

                        Vector3[] rot = { new Vector3(0, 0, 0), new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, -90, 0) };

                        for (int r = 0; r < _currentShipData.AllowedRotations.Length; r++)
                        {
                            List<int> allowedRotationsList = new List<int> { 0, 1, 2, 3 };
                            int randomRotation = allowedRotationsList[Random.Range(0, 4)];

                            pa.transform.rotation = Quaternion.Euler(_currentShipData.AllowedRotations[randomRotation]);

                            if (CanPlaceShip(pa.transform))
                            {
                                sameShipTypePlaced++;
                                posFound = true;
                                break;
                            }
                            else
                                allowedRotationsList.Remove(randomRotation);
                        }
                    }
                }
            }
        }
        #endregion

        //temporarily public
        public void PlaceShips() //REFACTOR!
        {
            ClearAllShips();

            bool posFound = false;

            for (int i = 0; i < _shipsToPlace.Count; i++)
            {
                int sameShipTypePlaced = 0;

                _currentShip = i;
                _currentShipData = _shipsToPlace[_currentShip].ShipData;

                for (int j = 0; j < _currentShipData.AmountToPlace; j++)
                {
                    if (sameShipTypePlaced == _currentShipData.AmountToPlace) return;

                    posFound = false;

                    while (!posFound)
                    {
                        int xPos = Random.Range(0, _xRange);
                        int zPos = Random.Range(0, _zRange);

                        GameObject pa = Instantiate(_currentShipData.PlacingAssisstant);
                        pa.transform.position = new Vector3(_board.transform.position.x + xPos, 1f, _board.transform.position.z + zPos);

                        for (int r = 0; r < _currentShipData.AllowedRotations.Length; r++)
                        {
                            List<int> allowedRotationsList = new List<int> { 0, 1, 2, 3 };
                            int randomRotation = allowedRotationsList[Random.Range(0, allowedRotationsList.Count)];

                            pa.transform.rotation = Quaternion.Euler(_currentShipData.AllowedRotations[randomRotation]);

                            if (CanPlaceShip(pa.transform))
                            {
                                InstantiateShipObject(pa);
                                sameShipTypePlaced++;
                                posFound = true;
                                break;
                            }
                            else if (allowedRotationsList.Count == 0)
                                Destroy(pa); //POOL THEM
                            else
                                allowedRotationsList.Remove(randomRotation);
                        }
                    }
                }
            }
        }

        bool CanPlaceShip(Transform t)
        {
            foreach (Transform child in t)
            {
                PlacingAssisstant pa = child.GetComponent<PlacingAssisstant>();

                if (!pa.IsOverTile())
                    return false;
            }

            return true;
        }

        void InstantiateShipObject(GameObject assisstant)
        {
            Vector3 pos = new Vector3(assisstant.transform.position.x, 0.6f, assisstant.transform.position.z);

            GameObject newShip = Instantiate(_currentShipData.ShipPrefab,
                                            pos,
                                            assisstant.transform.rotation);

            //UpdateGrid(assisstant.transform, newShip.GetComponent<Ship>());
            _spawnedShips.Add(newShip);
            //Store ship position in the active player's ship list

            Destroy(assisstant);
        }

        void ClearAllShips()
        {
            if (_spawnedShips.Count == 0)
                return;

            foreach (var ship in _spawnedShips)
            {
                Destroy(ship);
            }
        }

        void ResetGrid()
        {
            for (int x = 0; x < _xRange; x++)
            {
                for (int z = 0; z < _zRange; z++)
                {
                    TileOccupationType ot = TileOccupationType.EMPTY;
                    _tempGrid[x, z] = new TileInfo(ot, null);
                }
            }
        }

        void UpdateGrid(Transform shipTransform, Ship ship)
        {
            foreach (Transform child in shipTransform)
            {
                Tile tile = child.GetComponent<PlacingAssisstant>().GetTile();
                _tempGrid[tile.XPos, tile.ZPos] = new TileInfo(TileOccupationType.SHIP, ship);
            }
        }

        public bool CheckIfOccupied(int xPos, int zPos)
        {
            return _tempGrid[xPos, zPos].IsOccupied();
        }

    }
}
