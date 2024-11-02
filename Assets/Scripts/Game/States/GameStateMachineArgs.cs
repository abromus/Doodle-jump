namespace DoodleJump.Game.States
{
    internal struct GameStateMachineArgs
    {
        internal Worlds.IWorld World { get; set; }

        internal Worlds.Entities.IDoodler Doodler { get; set; }
    }
}
