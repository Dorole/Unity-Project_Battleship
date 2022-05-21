using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Battleship
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] GameObject _panel;
        [SerializeField] TextMeshProUGUI _playerText;
        [SerializeField] TextMeshProUGUI[] _boardTexts;

        private void Start()
        {
            _panel.SetActive(false);
        }

        public void TogglePanel()
        {
            _panel.SetActive(!_panel.activeSelf);
        }

        public void SetPlayerText(int index) //razmisli: mogucnost upisivanja imena igraca u meniju?
        {
            _playerText.text = $"PLAYER {index}";
        }

        public void SetPlayersTextOpacity(int activePlayer, float playerAlpha, int opponent, float opponentAlpha)
        {
            _boardTexts[activePlayer].color = new Color(1, 1, 1, playerAlpha);
            _boardTexts[opponent].color = new Color(1, 1, 1, opponentAlpha);
        }

        
    }
}
