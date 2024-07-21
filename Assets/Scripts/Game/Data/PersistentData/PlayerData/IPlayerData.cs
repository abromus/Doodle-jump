using System;

namespace DoodleJump.Game.Data
{
    internal interface IPlayerData : IPersistentData
    {
        public int CurrentScore { get; }

        public int MaxScore { get; }

        public event Action ScoreChanged;

        public void SetCurrentScore(int score);
    }
}
