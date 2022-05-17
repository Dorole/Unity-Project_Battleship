using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    [CreateAssetMenu (fileName = "TileData", menuName = "Tile Data", order = 51)]
    public class SO_TileData : ScriptableObject
    {
        [SerializeField] Sprite _defaultSprite;
        [SerializeField] Sprite _targetedSprite;

        public Sprite DefaultSprite => _defaultSprite;
        public Sprite TargetedSprite => _targetedSprite;

        //hit and sink vis rep
    }
}
