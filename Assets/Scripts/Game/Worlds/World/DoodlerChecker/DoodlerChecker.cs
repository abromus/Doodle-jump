using DoodleJump.Game.Data;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class DoodlerChecker : IDoodlerChecker
    {
        private int _maxHeight;

        private readonly IWorldData _worldData;
        private readonly IPlayerData _playerData;
        private readonly Transform _doodlerTransform;
        private readonly float _doodlerWidth;
        private readonly Transform _cameraTransform;
        private readonly Rect _screenRect;
        private readonly float _offset;

        internal DoodlerChecker(IWorldData worldData, IPersistentDataStorage persistentDataStorage, Transform doodlerTransform, float doodlerWidth, Transform cameraTransform, Rect screenRect)
        {
            _worldData = worldData;
            _playerData = persistentDataStorage.GetPlayerData();
            _doodlerTransform = doodlerTransform;
            _doodlerWidth = doodlerWidth;
            _cameraTransform = cameraTransform;
            _screenRect = screenRect;
            _offset = _screenRect.height * Constants.Half;
        }

        public void Restart()
        {
            _maxHeight = 0;
            _playerData.SetCurrentScore(0);
        }

        public void Tick()
        {
            CheckDoodlerPosition();
            UpdateScore();
        }

        private void CheckDoodlerPosition()
        {
            var doodlerPosition = _doodlerTransform.position;
            var doodlerXPosition = doodlerPosition.x;
            var width = _screenRect.width;

            if (doodlerXPosition < _screenRect.xMin)
                ChangeXPosition(_doodlerTransform, width);
            else if (_screenRect.xMax < doodlerXPosition)
                ChangeXPosition(_doodlerTransform, -width);

            if (_cameraTransform.position.y - doodlerPosition.y < _offset - _doodlerWidth)
                return;

            _worldData.GameOver(GameOverType.Falling);
        }

        private void UpdateScore()
        {
            var cameraPosition = Mathf.FloorToInt(_cameraTransform.position.y);

            if (cameraPosition < _maxHeight + 1)
                return;

            var delta = cameraPosition - _maxHeight;
            _maxHeight = cameraPosition;
            _playerData.SetCurrentScore(_playerData.CurrentScore + delta);
        }

        private void ChangeXPosition(Transform transform, float offset)
        {
            var position = transform.position;
            position.x += offset;
            transform.position = position;
        }
    }
}
