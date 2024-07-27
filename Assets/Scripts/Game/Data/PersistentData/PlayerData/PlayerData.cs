using System;
using DoodleJump.Core;

namespace DoodleJump.Game.Data
{
    internal sealed class PlayerData : IPlayerData
    {
        private int _currentScore;
        private int _maxScore;

        public int CurrentScore => _currentScore;

        public int MaxScore => _maxScore;

        public event Action ScoreChanged;

        public void SetCurrentScore(int score)
        {
            if (_currentScore == score)
                return;

            _currentScore = score;

            if (_maxScore < _currentScore)
                _maxScore = _currentScore;

            ScoreChanged.SafeInvoke();
        }
    }
}
