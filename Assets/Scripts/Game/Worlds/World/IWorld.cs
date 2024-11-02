namespace DoodleJump.Game.Worlds
{
    internal interface IWorld : Core.IDestroyable, Core.Services.IUpdatable, Core.Services.ILateUpdatable
    {
        public IWorldData WorldData { get; }

        public void Init(Data.IGameData gameData, in WorldArgs args);

        public void Prepare();

        public void GameOver(GameOverType type);

        public void Restart();
    }
}
