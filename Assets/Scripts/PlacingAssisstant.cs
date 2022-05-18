using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class PlacingAssisstant : MonoBehaviour
    {
        [SerializeField] LayerMask _layerToCheck;
        RaycastHit _hit;
        Tile _tile;
        GameObject _board;

        public void SetBoard(GameObject board) => _board = board;

        public bool IsOverTile()
        {
            _tile = GetTile();

            if (_tile != null) return true;

            //_tile = null;
            return false;
        }

        public Tile GetTile()
        {
            Ray ray = new Ray(transform.position, -transform.up);

            if (Physics.Raycast(ray, out _hit, 1f, _layerToCheck))
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.green);
                return _hit.collider.GetComponent<Tile>();
            }

            return null;
        }

    }
}
