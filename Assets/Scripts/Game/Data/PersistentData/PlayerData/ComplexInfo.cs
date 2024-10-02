namespace DoodleJump.Game.Data
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal readonly struct ComplexInfo
    {
        private readonly Worlds.Boosters.BoosterType _boosterType;
        private readonly int _count;

        internal readonly Worlds.Boosters.BoosterType BoosterType => _boosterType;

        internal readonly int Count => _count;

        internal ComplexInfo(Worlds.Boosters.BoosterType boosterType, int count)
        {
            _boosterType = boosterType;
            _count = count;
        }
    }
}
