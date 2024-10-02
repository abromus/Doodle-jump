namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerBoosterStorage : IDoodlerBoosterStorage
    {
        private readonly Data.IPlayerData _playerData;

        private readonly IDoodlerBoosterExecutor _doodlerBoosterExecutor;

        internal DoodlerBoosterStorage(in DoodlerBoosterStorageArgs args)
        {
            _playerData = args.PlayerData;

            _doodlerBoosterExecutor = new DoodlerBoosterExecutor(in args);

            _playerData.BoosterUsed += OnBoosterUsed;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Add(Worlds.Boosters.IBoosterCollisionInfo info, int count = 1)
        {
            _playerData.AddBooster(info, count);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool Has(Worlds.Boosters.BoosterType boosterType)
        {
            return _doodlerBoosterExecutor.Has(boosterType);
        }

        public void Destroy()
        {
            _playerData.BoosterUsed -= OnBoosterUsed;

            _doodlerBoosterExecutor.Destroy();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private bool Use(Worlds.Boosters.BoosterType boosterType)
        {
            return _doodlerBoosterExecutor.Execute(boosterType);
        }

        private void OnBoosterUsed(Worlds.Boosters.BoosterType boosterType)
        {
            if (Use(boosterType))
                _playerData.RemoveBooster(boosterType);
        }
    }
}
