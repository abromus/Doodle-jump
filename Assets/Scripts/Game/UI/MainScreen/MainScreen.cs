using DoodleJump.Core.Services;
using DoodleJump.Core.Settings;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump.Game.UI
{
    internal sealed class MainScreen : ScreenBase
    {
        [Core.Separator(Core.CustomColor.Lime)]
        [SerializeField] private Button _buttonPause;
        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private TMP_Text _maxScore;

        private IPlayerData _playerData;
        private IScreenSystemService _screenSystemService;
        private IUpdater _updater;
        private IInputService _inputService;
        private IAudioService _audioService;
        private bool _initialized;

        public override void Init(IGameData gameData, IWorldData worldData, IScreenSystemService screenSystemService)
        {
            var coreData = gameData.CoreData;
            var coreServiceStorage = coreData.ServiceStorage;
            var gameServiceStorage = gameData.ServiceStorage;
            var saveLoadService = gameServiceStorage.GetSaveLoadService();
            var inputConfig = coreData.ConfigStorage.GetInputConfig();
            var audioConfig = gameData.ConfigStorage.GetAudioConfig();

            _playerData = saveLoadService.PersistentDataStorage.GetPlayerData();
            _screenSystemService = screenSystemService;
            _updater = coreServiceStorage.GetUpdater();
            _inputService = coreServiceStorage.GetInputService();
            _audioService = gameServiceStorage.GetAudioService();

            InitAudioService(audioConfig);
            InitInputService(inputConfig);
            UpdateMaxScore(_playerData.MaxScore);
            Subscribe();

            _initialized = true;
        }

        private void OnEnable()
        {
            if (_initialized)
                Subscribe();
        }

        private void OnDisable()
        {
            if (_initialized)
                Unsubscribe();
        }

        private void InitAudioService(IAudioConfig audioConfig)
        {
            _audioService.SetActiveBackgroundMusic(audioConfig.IsBackgroundMusicActive);
            _audioService.SetActiveSounds(audioConfig.IsSoundsActive);
            _audioService.SetBackgroundMusicVolume(audioConfig.BackgroundMusicVolume);
            _audioService.SetSoundsVolume(audioConfig.SoundVolume);
        }

        private void InitInputService(IInputConfig inputConfig)
        {
            _inputService.SetXSensitivity(inputConfig.CurrentXSensitivity);
        }

        private void Subscribe()
        {
            _buttonPause.onClick.AddListener(OnButtonPauseClicked);

            if (_playerData != null)
                _playerData.ScoreChanged += OnScoreChanged;
        }

        private void Unsubscribe()
        {
            _buttonPause.onClick.RemoveListener(OnButtonPauseClicked);

            if (_playerData != null)
                _playerData.ScoreChanged -= OnScoreChanged;
        }

        private void OnButtonPauseClicked()
        {
            _updater.SetPause(true);
            _screenSystemService.ShowScreen(ScreenType.Settings);
        }

        private void OnScoreChanged()
        {
            _currentScore.text = $"{_playerData.CurrentScore}";

            var maxScore = _playerData.MaxScore;

            if (0 < maxScore)
                UpdateMaxScore(maxScore);
        }

        private void UpdateMaxScore(int maxScore)
        {
            _maxScore.text = $"Max score: {maxScore}";
        }
    }
}
