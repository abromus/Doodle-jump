using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using DoodleJump.Game.Worlds;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump.Game.UI
{
    [System.Serializable]
    internal sealed class SettingsScreen : ScreenBase, IPausable
    {
        [Core.Separator(Core.CustomColor.Lime)]
        [SerializeField] private Button _buttonClose;
        [SerializeField] private Button _buttonRestart;
        [SerializeField] private Button _buttonExit;

        private IWorldData _worldData;
        private IUpdater _updater;

        public override void Init(IGameData gameData, IWorldData worldData, IScreenSystemService screenSystemService)
        {
            _worldData = worldData;
            _updater = gameData.CoreData.ServiceStorage.GetUpdater();

            SubscribeUpdater();
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

        private void Subscribe()
        {
            _buttonClose.onClick.AddListener(OnButtonCloseClicked);
            _buttonRestart.onClick.AddListener(OnButtonRestartClicked);
            _buttonExit.onClick.AddListener(OnButtonExitClicked);
        }

        private void Unsubscribe()
        {
            _buttonClose.onClick.RemoveListener(OnButtonCloseClicked);
            _buttonRestart.onClick.RemoveListener(OnButtonRestartClicked);
            _buttonExit.onClick.RemoveListener(OnButtonExitClicked);
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
    }
}
