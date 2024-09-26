namespace DoodleJump.Game.Settings
{
    internal readonly struct ChanceInfo
    {
        private readonly int _index;
        private readonly float _spawnChance;

        internal readonly int Index => _index;

        internal readonly float SpawnChance => _spawnChance;

        internal ChanceInfo(int index, float spawnChance)
        {
            _index = index;
            _spawnChance = spawnChance;
        }
    }
}
