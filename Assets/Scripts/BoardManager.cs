using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] ShipPlacer _shipPlacer;

        private void Start()
        {
            //ShipPlacer.OnAllShipsPlaced += HideShips;
        }

        private void Update() //TESTING
        {
            if (Input.GetKey(KeyCode.H))
                HideShips();
        }

        void HideShips()
        {
            List<GameObject> spawnedShips = _shipPlacer.SpawnedShips;
            Dictionary<GameObject, GameObject> shipsAndZones = _shipPlacer.ShipDictionary;

            foreach (var ship in spawnedShips)
            {
                foreach (Transform child in ship.transform)
                    child.gameObject.SetActive(false);

                GameObject zone;
                shipsAndZones.TryGetValue(ship, out zone);
                zone.SetActive(false);
            }
        }
    }
}
