using System.Collections;
using UnityEngine;

namespace Battleship
{
    internal class Win : State
    {
        public Win(GameFlowSystem gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            Debug.Log($"Player {GameManager.CurrentPlayer + 1} won!");

            GameManager.UI.SetPlayersTextOpacity(GameManager.CurrentPlayer, 1, GameManager.CurrentOpponent, 1);
            GameManager.UI.SetDisplayWinPanel(GameManager.CurrentPlayer);

            yield break;
        }
    }
}