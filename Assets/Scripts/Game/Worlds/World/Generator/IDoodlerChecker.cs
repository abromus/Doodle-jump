using System;

namespace DoodleJump.Game
{
    internal interface IDoodlerChecker
    {
        public event Action GameOver;

        public void Tick();

        public void Restart();
    }
}
