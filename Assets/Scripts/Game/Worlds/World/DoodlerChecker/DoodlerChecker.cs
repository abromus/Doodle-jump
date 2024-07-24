﻿using DoodleJump.Game.Data;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class DoodlerChecker : IDoodlerChecker
    {
        private readonly IWorldData _worldData;
        private readonly IPlayerData _playerData;
        private readonly Transform _doodlerTransform;
        private readonly float _doodlerSize;
        private readonly Transform _cameraTransform;
        private readonly Rect _screenRect;
        private readonly float _offset;

        private readonly float _half = 0.5f;

        internal DoodlerChecker(IWorldData worldData, IPersistentDataStorage persistentDataStorage, Transform doodlerTransform, float doodlerSize, Transform cameraTransform, Rect screenRect)
        {
            _worldData = worldData;
            _playerData = persistentDataStorage.GetPlayerData();
            _doodlerTransform = doodlerTransform;
            _doodlerSize = doodlerSize;
            _cameraTransform = cameraTransform;
            _screenRect = screenRect;
            _offset = _screenRect.height * _half;
        }

        public void Tick()
        {
            CheckDoodlerPosition();
            UpdateScore();
        }

        public void Restart()
        {
            _playerData.SetCurrentScore(0);
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

            if (_cameraTransform.position.y - doodlerPosition.y < _offset - _doodlerSize)
                return;

            _worldData.Restart();
        }

        private void UpdateScore()
        {
            var cameraPosition = _cameraTransform.position.y;

            if (_playerData.CurrentScore < cameraPosition)
                _playerData.SetCurrentScore(Mathf.FloorToInt(cameraPosition));
        }

        private void ChangeXPosition(Transform transform, float offset)
        {
            var position = transform.position;
            position.x += offset;
            transform.position = position;
        }
    }
}
