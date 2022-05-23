using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class Replay : MonoBehaviour
    {
        private List<ReplayRecord> _replayRecords = new List<ReplayRecord>();

        private void Start()
        {
            _replayRecords.Add(new ReplayRecord { position = transform.position, rotation = transform.rotation});
        }
    }
}
