namespace DoodleJump.Game.Entities
{
    internal interface IDoodler : IEntity
    {
        public void Init(DoodlerArgs args);

        public void Jump(float height);
    }
}
