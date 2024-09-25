namespace DoodleJump.Game.Factories
{
    internal interface IDoodlerFactory : Core.Factories.IFactory
    {
        public void Init(Worlds.Entities.DoodlerArgs args);

        public Worlds.Entities.IDoodler Create();
    }
}
