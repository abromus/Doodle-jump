namespace DoodleJump.Core.Services
{
    public interface IUpdater : IService
    {
        public void AddUpdatable(IUpdatable updatable);

        public void RemoveUpdatable(IUpdatable updatable);

        public void AddFixedUpdatable(IFixedUpdatable updatable);

        public void RemoveFixedUpdatable(IFixedUpdatable updatable);

        public void AddLateUpdatable(ILateUpdatable updatable);

        public void RemoveLateUpdatable(ILateUpdatable updatable);

        public void Tick(float deltaTime);

        public void FixedTick(float deltaTime);

        public void LateTick(float deltaTime);
    }
}
