namespace DoodleJump.Game.UI
{
    internal interface IScreen
    {
        public ScreenType ScreenType { get; }

        public void Show();

        public void Hide();
    }
}
