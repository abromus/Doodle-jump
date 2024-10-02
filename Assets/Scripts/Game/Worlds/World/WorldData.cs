using System;
using DoodleJump.Core;

namespace DoodleJump.Game.Worlds
{
    internal sealed class WorldData : IWorldData
    {
        public event Action GameOver;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Restart()
        {
            GameOver.SafeInvoke();
        }
    }
}
