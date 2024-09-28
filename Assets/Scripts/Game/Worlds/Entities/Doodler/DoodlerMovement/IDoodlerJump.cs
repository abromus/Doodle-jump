namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerJump
    {
        public event System.Action Jumped;

        public void Do(float height);
    }
}
