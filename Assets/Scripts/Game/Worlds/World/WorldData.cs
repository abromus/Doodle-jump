using DoodleJump.Core;

namespace DoodleJump.Game.Worlds
{
    internal sealed class WorldData : IWorldData
    {
        public event System.Action GameStarted;

        public event System.Action<GameOverType> GameOvered;

        public event System.Action GameRestarted;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetGameStarted()
        {
            GameStarted.SafeInvoke();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetGameOvered(GameOverType type)
        {
            GameOvered.SafeInvoke(type);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetGameRestarted()
        {
            GameRestarted.SafeInvoke();
        }
    }
}
