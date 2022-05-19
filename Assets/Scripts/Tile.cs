using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public enum TileOccupationType
    {
        EMPTY, 
        SHIP
    }

    public class Tile : MonoBehaviour
    {
        [SerializeField] SO_TileData _tileData;
        [SerializeField] SpriteRenderer _sprite;

        public int XPos { get; set; }
        public int ZPos { get; set; }

       
        void OnMouseOver() => ActivateHighlight(_tileData.TargetedSprite);

        void OnMouseExit() => ActivateHighlight(_tileData.DefaultSprite);
        
        void ActivateHighlight(Sprite sprite)
        {
            _sprite.sprite = sprite;
        }

    }

    public class TileInfo
    {
        TileOccupationType _tileOccupationType;
        Ship _ship;

        public TileInfo(TileOccupationType occupationType, Ship ship)
        {
            _tileOccupationType = occupationType;
            _ship = ship;
        }
        public bool IsOccupied()
        {
            return _tileOccupationType == TileOccupationType.SHIP;
        }

    }
}
