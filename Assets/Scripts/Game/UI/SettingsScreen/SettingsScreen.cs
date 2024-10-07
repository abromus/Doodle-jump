using DoodleJump.Core.Services;
using DoodleJump.Core.Settings;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;

namespace DoodleJump.Game.UI
{
    internal sealed class SettingsScreen : BaseScreen, IPausable
    {
        [Core.Separator(Core.CustomColor.Lime)]
        [UnityEngine.SerializeField] private UnityEngine.UI.Button[] _buttonCloses;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonRestart;
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _buttonExit;
        [Core.Separator(Core.CustomColor.MediumTurquoise)]
        [UnityEngine.SerializeField] private UnityEngine.UI.Toggle _toggleBackgroundMusic;
        [UnityEngine.SerializeField] private UnityEngine.UI.Toggle _toggleSounds;
        [UnityEngine.SerializeField] private UnityEngine.UI.Slider _sliderBackgroundMusic;
        [UnityEngine.SerializeField] private UnityEngine.UI.Slider _sliderSounds;
        [Core.Separator(Core.CustomColor.Elsie)]
        [UnityEngine.SerializeField] private UnityEngine.UI.Slider _sliderXSensitivity;
        [Core.Separator(Core.CustomColor.Presley)]
        [UnityEngine.SerializeField] private UnityEngine.GameObject _qualitySettingsContainer;
        [UnityEngine.SerializeField] private UnityEngine.UI.Toggle _toggleVerticalSync;
        [UnityEngine.SerializeField] private UnityEngine.UI.Toggle _toggleUnlimitedFps;
        [UnityEngine.SerializeField] private TMPro.TMP_Text _labelFps;
        [UnityEngine.SerializeField] private UnityEngine.UI.Slider _sliderFps;

        private Worlds.IWorldData _worldData;
        private IUpdater _updater;
        private IInputService _inputService;
        private IQualityService _qualityService;
        private IAudioService _audioService;

        public override void Init(Data.IGameData gameData, Worlds.IWorldData worldData, IScreenSystemService screenSystemService)
        {
            _worldData = worldData;

            var coreServiceStorage = gameData.CoreData.ServiceStorage;
            _updater = coreServiceStorage.GetUpdater();
            _inputService = coreServiceStorage.GetInputService();
            _qualityService = coreServiceStorage.GetQualityService();
            _audioService = gameData.ServiceStorage.GetAudioService();

            SubscribeUpdater();

            var audioConfig = gameData.ConfigStorage.GetAudioConfig();
            var inputConfig = gameData.CoreData.ConfigStorage.GetInputConfig();
            var qualityConfig = gameData.CoreData.ConfigStorage.GetQualityConfig();

            InitAudioService(audioConfig);
            InitInputService(inputConfig);
            InitQualityService(qualityConfig);
        }

        public override void Hide()
        {
            base.Hide();

            _updater.SetPause(false);
        }

        public void SetPause(bool isPaused)
        {
            if (isPaused)
                return;

            _updater.SetPause(true);
        }

        private void OnEnable()
        {
            Subscribe();
            SubscribeUpdater();
        }

        private void OnDisable()
        {
            Unsubscribe();
            UnsubscribeUpdater();
        }

        private void InitAudioService(IAudioConfig audioConfig)
        {
            _toggleBackgroundMusic.isOn = audioConfig.IsBackgroundMusicActive;
            _toggleSounds.isOn = audioConfig.IsSoundsActive;
            _sliderBackgroundMusic.value = audioConfig.BackgroundMusicVolume;
            _sliderSounds.value = audioConfig.SoundVolume;
        }

        private void InitInputService(IInputConfig inputConfig)
        {
            _sliderXSensitivity.minValue = inputConfig.MinXSensitivity;
            _sliderXSensitivity.maxValue = inputConfig.MaxXSensitivity;
            _sliderXSensitivity.value = inputConfig.CurrentXSensitivity;
        }

        private void InitQualityService(IQualityConfig qualityConfig)
        {
#if UNITY_EDITOR == false && UNITY_ANDROID
            _qualitySettingsContainer.SetActive(false);

            return;
#else
            _toggleVerticalSync.isOn = qualityConfig.IsVerticalSyncEnabled;
            _toggleUnlimitedFps.isOn = qualityConfig.IsFpsUnlimited;
            _sliderFps.wholeNumbers = true;
            _sliderFps.minValue = qualityConfig.MinFps;
            _sliderFps.maxValue = qualityConfig.MaxFps;
#endif
        }

        private void Subscribe()
        {
            foreach (var buttonClose in _buttonCloses)
                buttonClose.onClick.AddListener(OnButtonCloseClicked);

            _buttonRestart.onClick.AddListener(OnButtonRestartClicked);
            _buttonExit.onClick.AddListener(OnButtonExitClicked);

            _toggleBackgroundMusic.onValueChanged.AddListener(OnBackgroundMusicActiveChanged);
            _toggleSounds.onValueChanged.AddListener(OnSoundsActiveChanged);
            _sliderBackgroundMusic.onValueChanged.AddListener(OnBackgroundMusicVolumeChanged);
            _sliderSounds.onValueChanged.AddListener(OnSoundsVolumeChanged);

            _sliderXSensitivity.onValueChanged.AddListener(OnXSensitivityChanged);

            _toggleVerticalSync.onValueChanged.AddListener(OnVerticalSyncActiveChanged);
            _toggleUnlimitedFps.onValueChanged.AddListener(OnUnlimitedFpsActiveChanged);
            _sliderFps.onValueChanged.AddListener(OnFpsChanged);
        }

        private void Unsubscribe()
        {
            foreach (var buttonClose in _buttonCloses)
                buttonClose.onClick.RemoveListener(OnButtonCloseClicked);

            _buttonRestart.onClick.RemoveListener(OnButtonRestartClicked);
            _buttonExit.onClick.RemoveListener(OnButtonExitClicked);

            _toggleBackgroundMusic.onValueChanged.RemoveListener(OnBackgroundMusicActiveChanged);
            _toggleSounds.onValueChanged.RemoveListener(OnSoundsActiveChanged);
            _sliderBackgroundMusic.onValueChanged.RemoveListener(OnBackgroundMusicVolumeChanged);
            _sliderSounds.onValueChanged.RemoveListener(OnSoundsVolumeChanged);

            _sliderXSensitivity.onValueChanged.RemoveListener(OnXSensitivityChanged);

            _toggleVerticalSync.onValueChanged.RemoveListener(OnVerticalSyncActiveChanged);
            _toggleUnlimitedFps.onValueChanged.RemoveListener(OnUnlimitedFpsActiveChanged);
            _sliderFps.onValueChanged.RemoveListener(OnFpsChanged);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void SubscribeUpdater()
        {
            _updater?.AddPausable(this);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void UnsubscribeUpdater()
        {
            _updater?.RemovePausable(this);
        }

        private void OnButtonCloseClicked()
        {
            Hide();
        }

        private void OnButtonRestartClicked()
        {
            Hide();

            _worldData.SetGameOvered(Worlds.GameOverType.User);
        }

        private void OnButtonExitClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }

        private void OnBackgroundMusicActiveChanged(bool isActive)
        {
            _audioService.SetActiveBackgroundMusic(isActive);
        }

        private void OnSoundsActiveChanged(bool isActive)
        {
            _audioService.SetActiveSounds(isActive);
        }

        private void OnBackgroundMusicVolumeChanged(float volume)
        {
            _audioService.SetBackgroundMusicVolume(volume);
        }

        private void OnSoundsVolumeChanged(float volume)
        {
            _audioService.SetSoundsVolume(volume);
        }

        private void OnXSensitivityChanged(float volume)
        {
            _inputService.SetXSensitivity(volume);
        }

        private void OnVerticalSyncActiveChanged(bool isActive)
        {
            _qualityService.SetVerticalSyncActive(isActive);

            if (isActive)
            {
                _toggleUnlimitedFps.interactable = false;
                _sliderFps.interactable = false;
            }
            else
            {
                _toggleUnlimitedFps.interactable = true;
                _sliderFps.interactable = _toggleUnlimitedFps.isOn == false;
            }
        }

        private void OnUnlimitedFpsActiveChanged(bool isActive)
        {
            _qualityService.SetUnlimitedFps(isActive);

            _sliderFps.interactable = isActive == false;
        }

        private void OnFpsChanged(float fps)
        {
            _qualityService.SetFps((int)fps);
            _labelFps.text = fps.ToString();
        }
    }
}
