namespace DoodleJump.Game
{
    internal interface IGame : Core.IDestroyable
    {
        public Data.IGameData GameData { get; }

        public void Run();
    }
}
