using DoodleJump.Core.Services;
using DoodleJump.Core.Settings;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump.Game.UI
{
    internal sealed class SettingsScreen : BaseScreen, IPausable
    {
        [Core.Separator(Core.CustomColor.Lime)]
        [SerializeField] private Button _buttonClose;
        [SerializeField] private Button _buttonRestart;
        [SerializeField] private Button _buttonExit;
        [Core.Separator(Core.CustomColor.MediumTurquoise)]
        [SerializeField] private Toggle _toggleBackgroundMusic;
        [SerializeField] private Toggle _toggleSounds;
        [SerializeField] private Slider _sliderBackgroundMusic;
        [SerializeField] private Slider _sliderSounds;
        [Core.Separator(Core.CustomColor.Elsie)]
        [SerializeField] private Slider _sliderXSensitivity;

        private IWorldData _worldData;
        private IUpdater _updater;
        private IInputService _inputService;
        private IAudioService _audioService;

        public override void Init(IGameData gameData, IWorldData worldData, IScreenSystemService screenSystemService)
        {
            _worldData = worldData;

            var coreServiceStorage = gameData.CoreData.ServiceStorage;
            _updater = coreServiceStorage.GetUpdater();
            _inputService = coreServiceStorage.GetInputService();
            _audioService = gameData.ServiceStorage.GetAudioService();

            SubscribeUpdater();

            var audioConfig = gameData.ConfigStorage.GetAudioConfig();
            var inputConfig = gameData.CoreData.ConfigStorage.GetInputConfig();

            InitAudioService(audioConfig);
            InitInputService(inputConfig);
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

        private void Subscribe()
        {
            _buttonClose.onClick.AddListener(OnButtonCloseClicked);
            _buttonRestart.onClick.AddListener(OnButtonRestartClicked);
            _buttonExit.onClick.AddListener(OnButtonExitClicked);

            _toggleBackgroundMusic.onValueChanged.AddListener(OnBackgroundMusicActiveChanged);
            _toggleSounds.onValueChanged.AddListener(OnSoundsActiveChanged);
            _sliderBackgroundMusic.onValueChanged.AddListener(OnBackgroundMusicVolumeChanged);
            _sliderSounds.onValueChanged.AddListener(OnSoundsVolumeChanged);

            _sliderXSensitivity.onValueChanged.AddListener(OnXSensitivityChanged);
        }

        private void Unsubscribe()
        {
            _buttonClose.onClick.RemoveListener(OnButtonCloseClicked);
            _buttonRestart.onClick.RemoveListener(OnButtonRestartClicked);
            _buttonExit.onClick.RemoveListener(OnButtonExitClicked);

            _toggleBackgroundMusic.onValueChanged.RemoveListener(OnBackgroundMusicActiveChanged);
            _toggleSounds.onValueChanged.RemoveListener(OnSoundsActiveChanged);
            _sliderBackgroundMusic.onValueChanged.RemoveListener(OnBackgroundMusicVolumeChanged);
            _sliderSounds.onValueChanged.RemoveListener(OnSoundsVolumeChanged);

            _sliderXSensitivity.onValueChanged.RemoveListener(OnXSensitivityChanged);
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

            _worldData.GameOver(GameOverType.User);
        }

        private void OnButtonExitClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
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
    }
}
