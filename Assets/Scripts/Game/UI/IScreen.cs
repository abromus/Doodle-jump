namespace DoodleJump.Game.UI
{
    internal interface IScreen
    {
        public ScreenType ScreenType { get; }

        public abstract void Init(Data.IGameData gameData, Worlds.IWorldData worldData, Services.IScreenSystemService screenSystemService);

        public void SetArgs(Services.IScreenArgs args);

        public void Show();

        public void Hide();
    }
}
