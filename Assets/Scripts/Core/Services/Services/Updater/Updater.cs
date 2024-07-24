using System.Collections.Generic;

namespace DoodleJump.Core.Services
{
    internal sealed class Updater : IUpdater
    {
        private readonly List<IUpdatable> _updatables = new(256);
        private readonly List<IFixedUpdatable> _fixedUpdatables = new(256);
        private readonly List<ILateUpdatable> _lateUpdatables = new(256);
        private readonly List<IPausable> _pausables = new(256);

        public void AddUpdatable(IUpdatable updatable)
        {
            if (_updatables.Contains(updatable) == false)
                _updatables.Add(updatable);
        }

        public void RemoveUpdatable(IUpdatable updatable)
        {
            if (_updatables.Contains(updatable))
                _updatables.Remove(updatable);
        }

        public void AddFixedUpdatable(IFixedUpdatable updatable)
        {
            if (_fixedUpdatables.Contains(updatable) == false)
                _fixedUpdatables.Add(updatable);
        }

        public void RemoveFixedUpdatable(IFixedUpdatable updatable)
        {
            if (_fixedUpdatables.Contains(updatable))
                _fixedUpdatables.Remove(updatable);
        }

        public void AddLateUpdatable(ILateUpdatable updatable)
        {
            if (_lateUpdatables.Contains(updatable) == false)
                _lateUpdatables.Add(updatable);
        }

        public void RemoveLateUpdatable(ILateUpdatable updatable)
        {
            if (_lateUpdatables.Contains(updatable))
                _lateUpdatables.Remove(updatable);
        }

        public void AddPausable(IPausable pausable)
        {
            if (_pausables.Contains(pausable) == false)
                _pausables.Add(pausable);
        }

        public void RemovePausable(IPausable pausable)
        {
            if (_pausables.Contains(pausable))
                _pausables.Remove(pausable);
        }

        public void Tick(float deltaTime)
        {
            for (int i = 0; i < _updatables.Count; i++)
                _updatables[i].Tick(deltaTime);
        }

        public void FixedTick(float deltaTime)
        {
            for (int i = 0; i < _fixedUpdatables.Count; i++)
                _fixedUpdatables[i].FixedTick(deltaTime);
        }

        public void LateTick(float deltaTime)
        {
            for (int i = 0; i < _lateUpdatables.Count; i++)
                _lateUpdatables[i].LateTick(deltaTime);
        }

        public void SetPause(bool isPaused)
        {
            for (int i = 0; i < _pausables.Count; i++)
                _pausables[i].SetPause(isPaused);
        }

        public void Destroy()
        {
            _updatables.Clear();
            _fixedUpdatables.Clear();
            _lateUpdatables.Clear();
            _pausables.Clear();
        }
    }
}
