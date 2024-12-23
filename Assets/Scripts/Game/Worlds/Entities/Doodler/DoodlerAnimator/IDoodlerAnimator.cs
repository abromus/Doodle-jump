﻿namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerAnimator
    {
        public void Restart();

        public void Tick(float deltaTime);

        public void FixedTick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
