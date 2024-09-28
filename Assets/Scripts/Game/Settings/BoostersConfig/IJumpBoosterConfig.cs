namespace DoodleJump.Game.Settings
{
    internal interface IJumpBoosterConfig : IBoosterConfig
    {
        public float ExistenseTime { get; }

        public float JumpForceFactor { get; }
    }
}
