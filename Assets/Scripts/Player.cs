using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    [System.Serializable]
    public class Player
    {
        public List<GameObject> PlacedShipsList = new List<GameObject>();
        [SerializeField] GameObject _myBoard;
        //ref to Cinemachine camera setup

        public GameObject PlayerBoard => _myBoard;
    }
}
