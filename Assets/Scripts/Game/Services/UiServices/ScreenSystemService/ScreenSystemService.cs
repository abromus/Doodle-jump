using System.Collections.Generic;
using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Settings;
using DoodleJump.Game.UI;
using DoodleJump.Game.Worlds;
using UnityEngine;

namespace DoodleJump.Game.Services
{
    internal sealed class ScreenSystemService : UiService, IScreenSystemService
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private RectTransform _screenContainer;
        [SerializeField] private ScreenInfo[] _screenInfos;

        private IGameData _gameData;
        private IWorldData _worldData;
        private IScreenSystemConfig _config;

        private readonly Dictionary<ScreenType, ScreenBase> _screens = new(8);

        public override UiServiceType UiServiceType => UiServiceType.ScreenSystemService;

        public IScreenSystemConfig Config => _config;

        public void Init(IGameData gameData, IWorldData worldData)
        {
            _gameData = gameData;
            _worldData = worldData;
            _config = gameData.ConfigStorage.GetScreenSystemConfig();

            var camera = gameData.CoreData.ServiceStorage.GetCameraService().Camera;

            _canvas.worldCamera = camera;
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;

            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
        }

        public bool ShowScreen(ScreenType screenType)
        {
            if (_screens.ContainsKey(screenType))
            {
                _screens[screenType].Show();

                return true;
            }

            foreach (var info in _screenInfos)
            {
                if (info.ScreenType != screenType)
                    continue;

                var screen = Instantiate(info.ScreenPrefab, _screenContainer);
                screen.Init(_gameData, _worldData, this);
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
