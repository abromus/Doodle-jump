namespace DoodleJump.Game.Data
{
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
    internal struct SimpleInfo
    {
        public int Id { get; set; }

        public int CurrentScore { get; set; }

        public int MaxScore { get; set; }
    }
}
