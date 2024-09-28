namespace DoodleJump.Game.Settings
{
    internal interface IJetpackBoosterConfig : IBoosterConfig
    {
        public float ExistenseTime { get; }

        public float JumpForce { get; }
    }
}
