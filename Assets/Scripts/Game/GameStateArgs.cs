namespace DoodleJump.Game.States
{
    internal readonly struct GameStateArgs
    {
        private readonly Worlds.IWorld _world;
        private readonly Worlds.Entities.IDoodler _doodler;

        internal Worlds.IWorld World => _world;

        internal Worlds.Entities.IDoodler Doodler => _doodler;

        public GameStateArgs(Worlds.IWorld world, Worlds.Entities.IDoodler doodler)
        {
            _world = world;
            _doodler = doodler;
        }
    }
}
