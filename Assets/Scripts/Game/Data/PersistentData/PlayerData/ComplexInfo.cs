namespace DoodleJump.Game.Data
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct ComplexInfo
    {
        private readonly Worlds.Boosters.BoosterType _boosterType;
        private readonly int _count;

        public readonly Worlds.Boosters.BoosterType BoosterType => _boosterType;

        public readonly int Count => _count;

        public ComplexInfo(Worlds.Boosters.BoosterType boosterType, int count)
        {
            _boosterType = boosterType;
            _count = count;
        }
    }
}
