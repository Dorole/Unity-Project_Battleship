using UnityEngine;
using TMPro;

namespace Battleship
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] ImageFader _imageFader;
        [SerializeField] GameObject _preparePanel;
        [SerializeField] GameObject _winPanel;
        [SerializeField] TextMeshProUGUI _playerText;
        [SerializeField] TextMeshProUGUI _winnerText;
        [SerializeField] TextMeshProUGUI[] _boardTexts;

        void Start()
        {
            _preparePanel.SetActive(true);
            _winPanel.SetActive(false);
            SetPlayerText(0);
        }

        public void TogglePanel()
        {
            _preparePanel.SetActive(!_preparePanel.activeSelf);
        }

        public void SetPlayerText(int index) //razmisli: mogucnost upisivanja imena igraca u meniju?
        {
            _playerText.text = $"PLAYER {index + 1}";
        }

        public void SetPlayersTextOpacity(int activePlayer, float playerAlpha, int opponent, float opponentAlpha)
        {
            _boardTexts[activePlayer].color = new Color(1, 1, 1, playerAlpha);
            _boardTexts[opponent].color = new Color(1, 1, 1, opponentAlpha);
        }

        public void FadeImage()
        {
            _imageFader.FadeOut();
        }

        public void SetDisplayWinPanel(int index)
        {
            _winnerText.text = $"PLAYER {index + 1}";
            _winPanel.SetActive(true);
        }

        
    }
}
