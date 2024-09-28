using DoodleJump.Core;
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
        [Separator(CustomColor.Lime)]
        [SerializeField] private Button _buttonPause;
        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private TMP_Text _maxScore;
        [SerializeField] private RectTransform _boostersContainer;

        private IPlayerData _playerData;
        private IScreenSystemService _screenSystemService;
        private IUpdater _updater;
        private IInputService _inputService;
        private IAudioService _audioService;
        private IMainScreenConfig _config;
        private bool _initialized;

        private readonly System.Collections.Generic.Dictionary<Worlds.Boosters.BoosterType, IObjectPool<IUiBooster>> _boosterPools = new(16);
        private readonly System.Collections.Generic.Dictionary<Worlds.Boosters.BoosterType, IUiBooster> _boosters = new(32);

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
            _config = screenSystemService.Config.MainScreenConfig;

            InitAudioService(audioConfig);
            InitInputService(inputConfig);
            InitPools();
            LoadBoosters();
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

        private void OnDestroy()
        {
            foreach (var boosterInfo in _boosters)
            {
                var uiBooster = boosterInfo.Value;
                uiBooster.Clicked -= OnBoosterClicked;

                _boosterPools[boosterInfo.Key].Release(uiBooster);
            }

            _boosters.Clear();
            _boosterPools.Clear();
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

        private void InitPools()
        {
            var boosterViewInfos = _config.BoosterViewInfos;

            foreach (var boosterViewInfo in boosterViewInfos)
            {
                var uiBoosterPrefab = boosterViewInfo.UiBoosterPrefab;
                var boosterType = boosterViewInfo.BoosterType;

                if (_boosterPools.ContainsKey(boosterType) == false)
                    _boosterPools.Add(boosterType, new ObjectPool<IUiBooster>(() => CreateUiBooster(uiBoosterPrefab)));
            }
        }

        private void LoadBoosters()
        {
            var boosters = _playerData.Boosters;

            foreach (var boosterInfo in boosters)
                UpdateBooster(boosterInfo.Key, boosterInfo.Value);
        }

        private void UpdateMaxScore(int maxScore)
        {
            _maxScore.text = $"Max score: {maxScore}";
        }

        private IUiBooster CreateUiBooster<T>(T uiBoosterPrefab) where T : MonoBehaviour, IUiBooster
        {
            var uiBooster = Instantiate(uiBoosterPrefab, _boostersContainer);
            uiBooster.GameObject.RemoveCloneSuffix();

            return uiBooster;
        }

        private void UpdateBooster(Worlds.Boosters.BoosterType boosterType, int count)
        {
            if (_boosters.ContainsKey(boosterType))
            {
                var booster = _boosters[boosterType];

                booster.UpdateCount(count);

                if (count == 0)
                {
                    booster.Clear();

                    _boosterPools[boosterType].Release(booster);
                    _boosters.Remove(boosterType);
                }
            }
            else if (0 < count)
            {
                var uiBooster = _boosterPools[boosterType].Get();
                uiBooster.Init(boosterType, count);
                uiBooster.Clicked += OnBoosterClicked;

                _boosters.Add(boosterType, uiBooster);
            }
        }

        private void Subscribe()
        {
            _buttonPause.onClick.AddListener(OnButtonPauseClicked);

            if (_playerData != null)
            {
                _playerData.ScoreChanged += OnScoreChanged;
                _playerData.BoosterChanged += OnBoosterChanged;
            }
        }

        private void Unsubscribe()
        {
            _buttonPause.onClick.RemoveListener(OnButtonPauseClicked);

            if (_playerData != null)
            {
                _playerData.ScoreChanged -= OnScoreChanged;
                _playerData.BoosterChanged -= OnBoosterChanged;
            }
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

        private void OnBoosterChanged(Worlds.Boosters.BoosterType boosterType, int count)
        {
            UpdateBooster(boosterType, count);
        }

        private void OnBoosterClicked(IUiBooster uiBooster)
        {
            var boosterType = uiBooster.BoosterType;

            _playerData.UseBooster(boosterType);
        }
    }
}
