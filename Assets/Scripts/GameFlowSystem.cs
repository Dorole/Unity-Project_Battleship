using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleship
{
    public class GameFlowSystem : StateMachine
    {
        #region FIELDS AND CONSTRUCTORS
        public static GameFlowSystem Instance;

        [HideInInspector] public bool TurnEnded;
        int _currentPlayer = 0;
        int _currentOpponent = 1; 
        
        [SerializeField] Player[] _players = new Player[2];
        [SerializeField] GameUI _ui;
                
        public Player[] Players => _players;
        public int CurrentPlayer => _currentPlayer;
        public int CurrentOpponent => _currentOpponent;
        public GameUI UI => _ui;
#endregion
        
        private void Awake()
        {
            Instance = this;
        }

        public void OnPlayerReadyButton() 
        {
            TurnEnded = false;
            _ui.TogglePanel();
            SetState(new PlayerTurn(this));
        }

        public void SwitchPlayer()
        {
            _currentOpponent = _currentPlayer;

            _currentPlayer++;
            _currentPlayer %= 2;
            Debug.Log($"Switched to player {_currentPlayer}. Opponent {_currentOpponent}.");
        }

        public void MoveToNextStage(IEnumerator stage)
        {
            StartCoroutine(stage);
        }

        public void AddShipToPlayerList(int currentBoard, GameObject ship)
        {
            _players[currentBoard].PlacedShipsList.Add(ship);
        }

    }
}
