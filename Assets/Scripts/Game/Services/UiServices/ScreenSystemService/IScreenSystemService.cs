using DoodleJump.Core.Services;
using DoodleJump.Game.UI;
using UnityEngine;

namespace DoodleJump.Game.Services
{
    internal interface IScreenSystemService : IService
    {
        public void Init(Data.IGameData gameData, Worlds.IWorldData worldData);

        public bool ShowScreen(ScreenType screenType, IScreenArgs args = null);

        public void HideScreen(ScreenType screenType);

        public void AttachTo(Transform parent);
    }
}
