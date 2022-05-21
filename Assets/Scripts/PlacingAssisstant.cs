using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class PlacingAssisstant : MonoBehaviour
    {
        RaycastHit _hit;
        Tile _tile;
        GameObject _board; //NOT NEEDED?

        private void Update()
        {
            //TESTING
            if (Input.GetKeyDown(KeyCode.Space))
                GetTile();
        }

        public void SetBoard(GameObject board) => _board = board;

        public bool IsOverTile()
        {
            _tile = GetTile();

            if (_tile != null) 
                return true;

            _tile = null;
            return false;
        }

        public Tile GetTile()
        {
            Ray ray = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(ray, out _hit, 1f))
            {
                //Debug.Log(_hit.collider.gameObject.name);

                if (_hit.collider.gameObject.GetComponent<Tile>())
                {
                  //  Debug.Log($"{transform.parent.gameObject.name}: Found empty tile!");
                    return _hit.collider.GetComponent<Tile>();
                }
                else
                {
                   // Debug.Log($"{transform.parent.gameObject.name}: Found occupied tile.");
                    return null;
                }
            }
            else
            {
                //Debug.Log($"{transform.parent.gameObject.name}: No tile found.");
                return null;
            }
        }

    }
}
