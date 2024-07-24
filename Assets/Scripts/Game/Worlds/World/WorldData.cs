using System;
using DoodleJump.Core;

namespace DoodleJump.Game.Worlds
{
    internal sealed class WorldData : IWorldData
    {
        public event Action GameOver;

        public void Restart()
        {
            GameOver.SafeInvoke();
        }
    }
}
