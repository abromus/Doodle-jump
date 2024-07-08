using DoodleJump.Core.Settings;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    public interface ICameraConfig : IConfig
    {
        public Vector3 Offset { get; }
    }
}
