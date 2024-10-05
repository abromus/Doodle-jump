using DoodleJump.Core;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using DoodleJump.Game.Worlds;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump.Game.UI
{
    internal sealed class MenuScreen : BaseScreen
    {
        [Separator(CustomColor.Lime)]
        [SerializeField] private Button _buttonStart;

        private IWorldData _worldData;
        private IPlayerData _playerData;
        private bool _initialized;

        public override void Init(IGameData gameData, IWorldData worldData, IScreenSystemService screenSystemService)
        {
            _worldData = worldData;

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

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _buttonStart.onClick.AddListener(OnButtonStartClicked);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Unsubscribe()
        {
            _buttonStart.onClick.RemoveListener(OnButtonStartClicked);
        }

        private void OnButtonStartClicked()
        {
            _worldData.SetGameStarted();
        }
    }
}
