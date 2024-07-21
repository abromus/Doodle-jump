using DoodleJump.Game.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump.Game.UI
{
    [System.Serializable]
    internal sealed class MainScreen : ScreenBase
    {
        [Core.Separator(Core.CustomColor.Lime)]
        [SerializeField] private Button _settings;
        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private TMP_Text _maxScore;

        private IPlayerData _playerData;

        public override void Show(IPersistentDataStorage persistentDataStorage)
        {
            base.Show(persistentDataStorage);

            _playerData = persistentDataStorage.GetPlayerData();

            Subscribe();
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _settings.onClick.AddListener(OnSettingsClicked);

            if (_playerData != null)
                _playerData.ScoreChanged += OnScoreChanged;
        }

        private void Unsubscribe()
        {
            _settings.onClick.RemoveListener(OnSettingsClicked);

            if (_playerData != null)
                _playerData.ScoreChanged -= OnScoreChanged;
        }

        private void OnSettingsClicked()
        {
            Debug.Log("Settings clicked");
        }

        private void OnScoreChanged()
        {
            _currentScore.text = $"{_playerData.CurrentScore}";

            var maxScore = _playerData.MaxScore;

            if (0 < maxScore)
                _maxScore.text = $"Max score: {maxScore}";
        }
    }
}
