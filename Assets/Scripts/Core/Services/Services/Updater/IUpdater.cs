namespace DoodleJump.Core.Services
{
    public interface IUpdater : IService
    {
        public void AddUpdatable(IUpdatable updatable);

        public void RemoveUpdatable(IUpdatable updatable);

        public void AddFixedUpdatable(IFixedUpdatable updatable);

        public void RemoveFixedUpdatable(IFixedUpdatable updatable);

        public void Tick(float deltaTime);

        public void FixedTick(float deltaTime);
    }
}
