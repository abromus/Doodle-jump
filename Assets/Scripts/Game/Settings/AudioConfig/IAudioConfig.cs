namespace DoodleJump.Game.Settings
{
    internal interface IAudioConfig : Core.Settings.IConfig
    {
        public bool IsBackgroundMusicActive { get; }

        public bool IsSoundsActive { get; }

        public float BackgroundMusicVolume { get; }

        public float SoundVolume { get; }
    }
}
