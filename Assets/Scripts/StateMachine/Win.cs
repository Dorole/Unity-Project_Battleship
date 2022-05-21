using System.Collections;
using UnityEngine;

namespace Battleship
{
    internal class Win : State
    {
        public Win(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            Debug.Log($"Player {GameManager.CurrentPlayer + 1} won!");

            //prikazi sve brodove
            //win screen ili samo promijeni tekst iznad ploce
            //buttons: PLAY AGAIN, MAIN MENU, QUIT

            yield break;
        }
    }
}