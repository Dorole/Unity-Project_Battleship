using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class Ship : MonoBehaviour
    {
        public static event Action<int> OnShipDestroyed;

        [SerializeField] SO_ShipData _shipData;
        GameObject _noGoZone;
        int _shipID;
        int _hits;
        bool _isDestroyed;

        public SO_ShipData ShipData => _shipData;
        public int ShipID => _shipID;
        public GameObject Zone => _noGoZone;
        public bool ShipDestroyed => _isDestroyed;

        private void Start()
        {
            DefineZone();
            Tile.OnTileHit += HandleHit;
        }

        void DefineZone()
        {
            ShipPlacer shipPlacer = FindObjectOfType<ShipPlacer>();
            Dictionary<GameObject, GameObject> shipsAndZones = shipPlacer.ShipDictionary;

            shipsAndZones.TryGetValue(gameObject, out _noGoZone);
            _noGoZone.GetComponent<NoGoZone>().SetZoneID(_shipID);
        }

        public void AssignShipID(int id)
        {
            _shipID = id;
        }

        public void HandleHit(int id)
        {
            if (_shipID != id)
                return;

            Debug.Log($"Ship with ID {_shipID} reacted to tile's event.");
            _hits++;

            if (_hits == _shipData.ShipLength)
            {
                Debug.Log($"Ship {_shipID} destroyed!");
                OnShipDestroyed?.Invoke(_shipID);
                _isDestroyed = true;

                foreach (Transform child in transform)
                    child.gameObject.SetActive(true);

                Tile.OnTileHit -= HandleHit;
            }
        }

    }
}
