using System.Collections;
using UnityEngine;

namespace Battleship
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameObject[] _boards;
        BoardGenerator _boardGenerator;
        BoardManager _boardManager;

        [SerializeField] ShipPlacer _shipPlacer;
        [SerializeField] GameUI _gameUI;

        private void Awake()
        {
            if (!_shipPlacer)
                _shipPlacer = FindObjectOfType<ShipPlacer>();
        }

        private void Start()
        {
            SetupGame();
        }

        void SetupGame()
        {
            foreach (GameObject board in _boards)
            {
                _boardGenerator = board.GetComponent<BoardGenerator>();
                _boardGenerator.GenerateBoard();
            }

            _shipPlacer.SetUpBoards();

            foreach (GameObject board in _boards)
            {
                _boardManager = board.GetComponent<BoardManager>();
                _boardManager.DisableClickingTiles();
            }

            StartCoroutine(RevealScreen());
        }

        IEnumerator RevealScreen()
        {
            yield return new WaitForSeconds(2);
            _gameUI.FadeImage();
        }
    }
}
