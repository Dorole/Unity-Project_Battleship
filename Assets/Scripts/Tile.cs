using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class Tile : MonoBehaviour
    {
        public static event Action<int> OnTileHit;

        [SerializeField] SO_TileData _tileData;
        [SerializeField] SpriteRenderer _sprite;
        [SerializeField] Ship _ship;
        int _tileID; 
        MeshRenderer _mesh;
        bool _tileChecked;

        public int XPos { get; set; }
        public int ZPos { get; set; }

        private void Start()
        {
            _mesh = GetComponent<MeshRenderer>();
            ShipPlacer.OnBoardReset += ResetTile; //FOR TESTING
        }

        void OnMouseOver() => ActivateHighlight(_tileData.TargetedSprite);

        void OnMouseExit() => ActivateHighlight(_tileData.DefaultSprite);

        void OnMouseUp()
        {
            if (!_tileChecked)
                CheckForHit();
            else
                Debug.Log("You already checked this tile.");
        }

        void ActivateHighlight(Sprite sprite) 
        {
            if (!_tileChecked)
                _sprite.sprite = sprite;
            else
                _sprite.sprite = _tileData.DefaultSprite;
        }

        public void GetShipOnTile(Ship ship)
        {
            _ship = ship;
            _tileID = ship.ShipID;
        }

        void CheckForHit()
        {
            if (_ship != null)
            {
                _mesh.material = _tileData.HitMaterial;
                OnTileHit?.Invoke(_tileID);
            }
            else
                _mesh.material = _tileData.MissedMaterial;

            _tileChecked = true;
        }    

        void ResetTile() //FOR TESTING
        {
            _mesh.material = _tileData.DefaultMaterial;
            GetComponent<BoxCollider>().enabled = true;
        }

        public void MarkEmptyTile()
        {
            _mesh.material = _tileData.MissedMaterial;
            _tileChecked = true;
        }
    }
}
