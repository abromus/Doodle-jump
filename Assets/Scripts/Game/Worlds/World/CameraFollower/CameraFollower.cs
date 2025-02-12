﻿using DG.Tweening;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class CameraFollower : ICameraFollower
    {
        private bool _isCameraReset;
        private bool _canMove;
        private Sequence _sequence;

        private readonly UnityEngine.Transform _doodlerTransform;
        private readonly Core.Services.ICameraService _cameraService;
        private readonly UnityEngine.Transform _cameraTransform;
        private readonly UnityEngine.Transform _cameraLeftTransform;
        private readonly UnityEngine.Transform _cameraRightTransform;
        private readonly float _halfScreenHeight;
        private readonly UnityEngine.Vector3 _cameraOffset;
        private readonly float _animationDelay;
        private readonly float _animationDuration;

        internal CameraFollower(in CameraFollowerArgs args)
        {
            _doodlerTransform = args.DoodlerTransform;
            _cameraService = args.CameraService;
            _cameraTransform = args.CameraTransform;
            _cameraLeftTransform = args.CameraLeftTransform;
            _cameraRightTransform = args.CameraRightTransform;
            _cameraOffset = args.CameraOffset;
            _animationDelay = args.AnimationDelay;
            _animationDuration = args.AnimationDuration;

            var cameraService = args.CameraService;
            cameraService.AttachTo(args.WorldTransform);

            _halfScreenHeight = cameraService.GetScreenRect().height * Constants.Half;

            _cameraTransform.localScale = UnityEngine.Vector3.one;
            _cameraTransform.position = _cameraOffset;

            _cameraLeftTransform.gameObject.SetActive(false);
            _cameraRightTransform.gameObject.SetActive(false);
        }

        public void GameOver(GameOverType type)
        {
            if (type == GameOverType.User)
                return;

            _canMove = false;

            _cameraLeftTransform.gameObject.SetActive(false);
            _cameraRightTransform.gameObject.SetActive(false);

            var screenSize = _halfScreenHeight * 2f;

            _sequence = DOTween.Sequence();

            if (type == GameOverType.EnemyCollided)
                _sequence.AppendInterval(_animationDelay);

            _sequence.Append(_cameraTransform.DOLocalMoveY(_cameraTransform.localPosition.y - screenSize, _animationDuration));
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Restart()
        {
            _canMove = true;
            _sequence?.Kill();

            ResetCamera();

            _cameraLeftTransform.gameObject.SetActive(true);
            _cameraRightTransform.gameObject.SetActive(true);
        }

        public void LateTick(float deltaTime)
        {
            if (_canMove == false)
                return;

            var cameraPosition = _cameraTransform.position;
            var cameraPositionY = cameraPosition.y;
            var doodlerPositionY = _doodlerTransform.position.y;

            if (doodlerPositionY <= cameraPositionY && cameraPositionY - _halfScreenHeight <= doodlerPositionY)
                return;

            cameraPosition.y = doodlerPositionY;

            _cameraTransform.position = cameraPosition;
        }

        private void ResetCamera()
        {
            _cameraTransform.localScale = UnityEngine.Vector3.one;
            _cameraTransform.position = _cameraOffset;

            if (_isCameraReset)
                return;

            var screenRect = _cameraService.GetScreenRect();
            var halfWidth = screenRect.width;

            var cameraLeftPosition = _cameraLeftTransform.localPosition;
            cameraLeftPosition.x -= halfWidth;

            _cameraLeftTransform.localPosition = cameraLeftPosition;

            var cameraRightPosition = _cameraRightTransform.localPosition;
            cameraRightPosition.x += halfWidth;

            _cameraRightTransform.localPosition = cameraRightPosition;

            _isCameraReset = true;
        }
    }
}
