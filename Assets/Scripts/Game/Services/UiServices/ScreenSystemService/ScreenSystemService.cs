using System.Collections.Generic;
using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.UI;
using DoodleJump.Game.Worlds;
using UnityEngine;

namespace DoodleJump.Game.Services
{
    internal sealed class ScreenSystemService : BaseUiService, IScreenSystemService
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _screenContainer;
        [SerializeField] private ScreenInfo[] _screenInfos;

        private IGameData _gameData;
        private IWorldData _worldData;

        private readonly Dictionary<ScreenType, BaseScreen> _screens = new(8);

        public override UiServiceType UiServiceType => UiServiceType.ScreenSystemService;

        public void Init(IGameData gameData, IWorldData worldData)
        {
            _gameData = gameData;
            _worldData = worldData;

            var camera = gameData.CoreData.ServiceStorage.GetCameraService().Camera;

            _canvas.worldCamera = camera;
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;

            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
        }

        public bool ShowScreen(ScreenType screenType, IScreenArgs args = null)
        {
            if (_screens.ContainsKey(screenType))
            {
                var screen = _screens[screenType];
                screen.SetArgs(args);
                screen.Show();

                return true;
            }

            foreach (var info in _screenInfos)
            {
                if (info.ScreenType != screenType)
                    continue;

                var screen = Instantiate(info.ScreenPrefab, _screenContainer);
                screen.Init(_gameData, _worldData, this);
                screen.SetArgs(args);
                screen.Show();

                _screens.Add(screenType, screen);

                return true;
            }

            return false;
        }

        public void HideScreen(ScreenType screenType)
        {
            if (_screens.ContainsKey(screenType))
                _screens[screenType].Hide();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void AttachTo(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void Destroy()
        {
            foreach (var screen in _screens.Values)
                if (screen != null && screen.gameObject != null)
                    Destroy(screen.gameObject);

            _screens.Clear();
        }
    }
}
