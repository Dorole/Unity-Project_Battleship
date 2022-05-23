using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class ReplaySystem : MonoBehaviour
    {
        public static event Action OnReplayStarted;

        List<Vector3> _playedTileCoordinates = new List<Vector3>();
        bool _replayStarted;

        public void AddToTileList(Vector3 pos)
        {
            if (!_replayStarted)
                _playedTileCoordinates.Add(pos);
        }

        public void StartReplay() //button
        {
            //should somehow disable then enable tiles, ships and ngzones - who? BoardManager?
            _replayStarted = true;
            OnReplayStarted?.Invoke();
            //UPDATE OVDJE ILI KORUTINA NEW STATE?
            //DODAJ GUMBICE ZA STOP, CONTINUE I STRELICU ZA MOVE BY MOVE
            //RAYCAST MORA BITI UKLJUCEN ZBOG ZONA, DODAJ NEKI BLOCK ZA PLAYERA :-/
        }
    }
}
