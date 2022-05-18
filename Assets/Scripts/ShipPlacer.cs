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

        private void Start()
        {
            _xRange = _board.GetComponent<BoardGenerator>().BoardSizeX;
            _zRange = _board.GetComponent<BoardGenerator>().BoardSizeZ;
        }

        //temporarily public
        public void PlaceShips() //REFACTOR!
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
                        pa.transform.position = new Vector3(_board.transform.position.x + xPos, 0, _board.transform.position.z + zPos);

                        //CHECK PREFABS - SOMETHING WRONG WITH ROTATIONS!

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
            GameObject newShip = Instantiate(_currentShipData.ShipPrefab,
                                            assisstant.transform.position,
                                            assisstant.transform.rotation);

            //Store ship position in the active player's ship list

            //assisstant.SetActive(false);
            Destroy(assisstant);
        }

    }
}
