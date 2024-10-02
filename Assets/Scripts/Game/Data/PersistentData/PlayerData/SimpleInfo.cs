namespace DoodleJump.Game.Data
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal struct SimpleInfo
    {
        internal int Id { get; set; }

        internal int CurrentScore { get; set; }

        internal int MaxScore { get; set; }

        internal int CurrentShots { get; set; }

        internal int MaxShots { get; set; }
    }
}
