namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerBoosterExecutor : IDoodlerBoosterExecutor
    {
        private readonly DoodlerBoosterStorageArgs _args;
        private readonly Factories.IBoosterFactory _factory;
        private readonly Settings.IBoostersConfig _boostersConfig;
        private readonly UnityEngine.Transform _boosterContainer;
        private readonly System.Collections.Generic.List<Boosters.IBooster> _boosters = new(16);
        private readonly System.Collections.Generic.List<Boosters.IBooster> _executingBoosters = new(16);
        private readonly System.Collections.Generic.Dictionary<Worlds.Boosters.BoosterType, Core.IObjectPool<Boosters.IBooster>> _pools = new(16);

        internal DoodlerBoosterExecutor(in DoodlerBoosterStorageArgs args)
        {
            _args = args;
            _factory = _args.BoosterFactory;
            _boosterContainer = _args.BoosterContainer;
            _boostersConfig = _args.BoostersConfig;

            InitPools();
        }

        public bool Execute(Worlds.Boosters.BoosterType boosterType)
        {
            if (CanUse(boosterType) == false)
                return false;

            var booster = GetBooster(boosterType);
            booster.Executed += OnExecuted;

            _boosters.Add(booster);
            _executingBoosters.Add(booster);

            booster.Execute();

            return true;
        }

        public bool Has(Worlds.Boosters.BoosterType boosterType)
        {
            foreach (var booster in _boosters)
                if (booster.BoosterType == boosterType)
                    return true;

            return false;
        }

        public void Destroy()
        {
            var count = _boosters.Count;

            for (int i = count - 1; 0 < i + 1; i--)
                DestroyBooster(_boosters[i]);
        }

        private void DestroyBooster(Boosters.IBooster booster)
        {
            booster.Executed -= OnExecuted;
            booster.Destroy();

            _pools[booster.BoosterType].Release(booster);
            _boosters.Remove(booster);

            if (_executingBoosters.Contains(booster))
                _executingBoosters.Remove(booster);
        }

        private void InitPools()
        {
            var boosterConfigs = _boostersConfig.BoosterConfigs;

            foreach (var boosterConfig in boosterConfigs)
            {
                var prefab = boosterConfig.BoosterPrefab;

                if (prefab == null)
                    continue;

                var boosterType = prefab.BoosterType;

                if (_pools.ContainsKey(boosterType))
                    continue;

                _pools.Add(boosterType, new Core.ObjectPool<Boosters.IBooster>(() => CreateBooster(boosterConfig, prefab)));
            }
        }

        private Boosters.IBooster CreateBooster<T>(Settings.IBoosterConfig config, T prefab) where T : UnityEngine.MonoBehaviour, Boosters.IBooster
        {
            var booster = _factory.Create(prefab, _boosterContainer);
            booster.Init(config, in _args);

            return booster;
        }

        private Boosters.IBooster GetBooster(Worlds.Boosters.BoosterType boosterType)
        {
            var booster = _pools[boosterType].Get();

            return booster;
        }

        private bool CanUse(Worlds.Boosters.BoosterType boosterType)
        {
            if (Has(boosterType))
                return false;

            switch (boosterType)
            {
                case Worlds.Boosters.BoosterType.Shield:
                    return Has(Worlds.Boosters.BoosterType.Jump) == false && Has(Worlds.Boosters.BoosterType.Jetpack) == false;
                case Worlds.Boosters.BoosterType.Jump:
                    return Has(Worlds.Boosters.BoosterType.Jetpack) == false;
                case Worlds.Boosters.BoosterType.Jetpack:
                    return Has(Worlds.Boosters.BoosterType.Jump) == false;
                default:
                    break;
            }

            return true;
        }

        private void OnExecuted(Boosters.IBooster booster)
        {
            DestroyBooster(booster);
        }
    }
}
