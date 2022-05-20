using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class NoGoZone : MonoBehaviour
    {
        int _zoneID;

        private void Start()
        {
            Ship.OnShipDestroyed += MarkZone;
        }

        public void SetZoneID(int id)
        {
            _zoneID = id;
        }

        public void MarkZone(int id)
        {
            if (_zoneID != id)
                return;

            RaycastHit hit;

            foreach (Transform child in transform)
            {
                Ray ray = new Ray(child.position, Vector3.down);

                if (Physics.Raycast(ray, out hit, 10f))
                {
                    if (hit.collider.gameObject.GetComponent<Tile>())
                        hit.collider.GetComponent<Tile>().MarkEmptyTile();

                }
            }

            gameObject.SetActive(false);
        }
    }
}
