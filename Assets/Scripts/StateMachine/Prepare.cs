using System.Collections;
using UnityEngine;

namespace Battleship
{
    internal class Prepare : State
    {
        public Prepare(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            Debug.Log("Next player prepare!");
            GameManager.SwitchPlayer();
            GameManager.UI.SetPlayerText(GameManager.CurrentPlayer + 1);
            GameManager.UI.TogglePanel();
            yield break;
        }
    }
}