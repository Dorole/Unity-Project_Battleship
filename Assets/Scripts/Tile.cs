using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] SO_TileData _tileData;
        [SerializeField] SpriteRenderer _sprite;

        void OnMouseOver() => ActivateHighlight(_tileData.TargetedSprite);

        void OnMouseExit() => ActivateHighlight(_tileData.DefaultSprite);
        
        void ActivateHighlight(Sprite sprite)
        {
            _sprite.sprite = sprite;
        }
    }
}
