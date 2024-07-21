namespace DoodleJump.Game.UI
{
    internal interface IScreen
    {
        public ScreenType ScreenType { get; }

        public void Show(Data.IPersistentDataStorage persistentDataStorage);

        public void Hide();
    }
}
