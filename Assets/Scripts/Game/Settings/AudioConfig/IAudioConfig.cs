using DoodleJump.Core.Settings;

namespace DoodleJump.Game.Settings
{
    public interface IAudioConfig : IConfig
    {
        public bool IsBackgroundMusicActive { get; }

        public bool IsSoundsActive { get; }

        public float BackgroundMusicVolume { get; }

        public float SoundVolume { get; }
    }
}
