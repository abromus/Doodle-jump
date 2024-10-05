using System;
using DoodleJump.Core;

namespace DoodleJump.Game.Worlds
{
    internal sealed class WorldData : IWorldData
    {
        public event Action<GameOverType> GameOvered;

        public event Action GameRestarted;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void GameOver(GameOverType type)
        {
            GameOvered.SafeInvoke(type);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Restart()
        {
            GameRestarted.SafeInvoke();
        }
    }
}
