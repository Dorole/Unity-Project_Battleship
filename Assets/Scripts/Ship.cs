using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] SO_ShipData _shipData;
        public SO_ShipData ShipData => _shipData;


    }
}
