using DG.Tweening;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class CameraFollower : ICameraFollower
    {
        private bool _canMove;
        private Sequence _sequence;

        private readonly UnityEngine.Transform _doodlerTransform;
        private readonly Core.Services.ICameraService _cameraService;
        private readonly UnityEngine.Transform _cameraTransform;
        private readonly float _halfScreenHeight;
        private readonly UnityEngine.Vector3 _cameraOffset;
        private readonly float _animationDelay;
        private readonly float _animationDuration;

        internal CameraFollower(in CameraFollowerArgs args)
        {
            _doodlerTransform = args.DoodlerTransform;
            _cameraService = args.CameraService;
            _cameraTransform = args.CameraTransform;
            _cameraOffset = args.CameraOffset;
            _animationDelay = args.AnimationDelay;
            _animationDuration = args.AnimationDuration;

            var cameraService = args.CameraService;
            cameraService.AttachTo(args.WorldTransform);

            _halfScreenHeight = cameraService.GetScreenRect().height * Constants.Half;

            ResetCamera();
        }

        public void GameOver(GameOverType type)
        {
            if (type == GameOverType.User)
                return;

            _canMove = false;

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
        }
    }
}
