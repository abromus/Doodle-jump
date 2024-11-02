namespace DoodleJump.Game.Settings
{
    internal interface ICameraConfig : Core.Settings.IConfig
    {
        public UnityEngine.Vector3 Offset { get; }
    }
}
