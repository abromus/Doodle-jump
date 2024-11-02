namespace DoodleJump.Game.Services
{
    internal interface IScreenSystemService : Core.Services.IService
    {
        public void Init(Data.IGameData gameData, Worlds.IWorldData worldData);

        public bool ShowScreen(UI.ScreenType screenType, IScreenArgs args = null);

        public void HideScreen(UI.ScreenType screenType);

        public void AttachTo(UnityEngine.Transform parent);
    }
}
