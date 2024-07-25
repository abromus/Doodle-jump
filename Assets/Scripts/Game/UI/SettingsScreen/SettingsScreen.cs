using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump.Game.UI
{
    internal sealed class SettingsScreen : ScreenBase, IPausable
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

        private IWorldData _worldData;
        private IUpdater _updater;
        private IAudioService _audioService;

        public override void Init(IGameData gameData, IWorldData worldData, IScreenSystemService screenSystemService)
        {
            _worldData = worldData;
            _updater = gameData.CoreData.ServiceStorage.GetUpdater();
            _audioService = gameData.ServiceStorage.GetAudioService();

            SubscribeUpdater();

            var audioConfig = gameData.ConfigStorage.GetAudioConfig();

            InitAudioService(audioConfig);
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

        private void Subscribe()
        {
            _buttonClose.onClick.AddListener(OnButtonCloseClicked);
            _buttonRestart.onClick.AddListener(OnButtonRestartClicked);
            _buttonExit.onClick.AddListener(OnButtonExitClicked);

            _toggleBackgroundMusic.onValueChanged.AddListener(OnBackgroundMusicActiveChanged);
            _toggleSounds.onValueChanged.AddListener(OnSoundsActiveChanged);
            _sliderBackgroundMusic.onValueChanged.AddListener(OnBackgroundMusicVolumeChanged);
            _sliderSounds.onValueChanged.AddListener(OnSoundsVolumeChanged);
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
        }

        private void SubscribeUpdater()
        {
            _updater?.AddPausable(this);
        }

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

            _worldData.Restart();
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
    }
}
