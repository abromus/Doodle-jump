namespace DoodleJump.Game.Settings
{
    internal interface ISpringJumpConfig : IJumpConfig
    {
        public float SpringJumpForce { get; }
    }
}
