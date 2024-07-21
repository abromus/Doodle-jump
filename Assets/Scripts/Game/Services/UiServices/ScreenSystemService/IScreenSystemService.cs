using DoodleJump.Core.Services;
using DoodleJump.Game.UI;
using UnityEngine;

namespace DoodleJump.Game.Services
{
    internal interface IScreenSystemService : IService
    {
        public void Init(ICameraService cameraService, Data.IPersistentDataStorage persistentDataStorage);

        public bool ShowScreen(ScreenType screenType);

        public void HideScreen(ScreenType screenType);

        public void AttachTo(Transform parent);
    }
}
