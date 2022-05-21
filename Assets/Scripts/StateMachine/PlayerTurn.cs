using System.Collections;
using UnityEngine;

namespace Battleship
{
    internal class PlayerTurn : State
    {
        int _currentPlayer;
        GameObject _currentPlayerBoard;
        BoardManager _currentPlayerBoardManager;
        
        int _opponent;
        BoardManager _opponentBoardManager;

        public PlayerTurn(GameManager gameManager) : base(gameManager)
        {
            _currentPlayer = GameManager.CurrentPlayer;
            _currentPlayerBoard = GameManager.Players[_currentPlayer].PlayerBoard;
            _currentPlayerBoardManager = _currentPlayerBoard.GetComponent<BoardManager>();

            _opponent = GameManager.CurrentOpponent;
            _opponentBoardManager = GameManager.Players[_opponent].PlayerBoard.GetComponent<BoardManager>();
        }

        public override IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
           
            _currentPlayerBoardManager.ToggleBoardLayer(_currentPlayerBoardManager.LayerIgnoreRaycast);
           _opponentBoardManager.ToggleBoardLayer(_opponentBoardManager.LayerGameBoard); 
            
            Debug.Log($"Player {_currentPlayer} TURN STARTED. OPPONENT: PLAYER {_opponent}");
            //change camera

            DisplayCurrentPlayersShips();
            GameManager.MoveToNextStage(TakeTurn());
        }

        public override IEnumerator TakeTurn()
        {
            yield return new WaitUntil(() => GameManager.TurnEnded || WinCheck());

            Debug.Log("TURN OVER. CHECK FOR WIN OR SWITCH");
            GameManager.MoveToNextStage(Exit());
        }

        public override IEnumerator Exit()
        {
            if (WinCheck())
            {
                GameManager.SetState(new Win(GameManager));
                yield break;
            }
            
            Debug.Log("PLAYER SWITCH");
            HideUndestroyedShips();
            _opponentBoardManager.ToggleBoardLayer(_opponentBoardManager.LayerIgnoreRaycast);
            yield return new WaitForSeconds(1f);

            GameManager.SetState(new Prepare(GameManager));

        }

        void HideUndestroyedShips()
        {
            foreach (GameObject ship in GameManager.Players[_currentPlayer].PlacedShipsList)
            {
                if (!ship.GetComponent<Ship>().ShipDestroyed)
                {
                    foreach (Transform child in ship.transform)
                        child.gameObject.SetActive(false);
                }
            }
        }

        void DisplayCurrentPlayersShips()
        {
            foreach (GameObject ship in GameManager.Players[_currentPlayer].PlacedShipsList)
            {
                 foreach (Transform child in ship.transform)
                        child.gameObject.SetActive(true);
            }
        }

        bool WinCheck()
        {
            foreach (GameObject placedShip in GameManager.Players[_opponent].PlacedShipsList)
            {
                Ship shipInfo = placedShip.GetComponent<Ship>();

                if (!shipInfo.ShipDestroyed)
                    return false;
            }

            return true;
        }
    }
}