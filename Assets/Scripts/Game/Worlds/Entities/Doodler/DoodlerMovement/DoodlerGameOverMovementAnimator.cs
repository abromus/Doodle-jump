using DG.Tweening;
using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerGameOverMovementAnimator : IDoodlerGameOverMovementAnimator
    {
        private Sequence _sequence;

        private readonly Rigidbody2D _rigidbody;
        private readonly ICameraService _cameraService;
        private readonly DoodlerMovementAnimationArgs _args;

        internal DoodlerGameOverMovementAnimator(Rigidbody2D rigidbody, ICameraService cameraService, in DoodlerMovementAnimationArgs args)
        {
            _rigidbody = rigidbody;
            _cameraService = cameraService;
            _args = args;
        }

        public void Start(GameOverType type)
        {
            if (type == GameOverType.User)
                return;

            _rigidbody.simulated = false;

            var transform = _rigidbody.transform;

            _sequence = DOTween.Sequence();

            var isEnemyCollided = type == GameOverType.EnemyCollided;
            var doodlerHeight = isEnemyCollided ? 2f : 1f;
            var downMoveDuration = _args.DownMoveDuration;
            var targetDownMoveDuration = isEnemyCollided ? downMoveDuration : downMoveDuration * 2f;

            if (isEnemyCollided)
                _sequence.Append(MoveAlongParabola(transform));

            _sequence.Append(MoveDown(transform, doodlerHeight, targetDownMoveDuration));

            Tween MoveAlongParabola(Transform transform)
            {
                var direction = transform.localScale.x == Constants.Right ? Constants.Right : Constants.Left;
                var startPosition = _rigidbody.transform.position;
                var targetPosition = startPosition;
                targetPosition.x = 0f;

                var duration = _args.ParabolaMoveDuration;
                var parabolaHeight = _args.ParabolaHeight;
                var startValue = 0f;
                var endValue = 1f;

                return DOTween.To(() => startValue, x => UpdatePosition(x), endValue, duration).SetEase(Ease.Linear);

                void UpdatePosition(float t)
                {
                    //Уравнение движения по параболе:
                    //y = parabolaHeight * (1 − (x − midX) / (endX − midX)) ^ 2)

                    var newX = Mathf.Lerp(startPosition.x, targetPosition.x, t);

                    var midX = (startPosition.x + targetPosition.x) * Constants.Half;
                    var newY = Mathf.Lerp(startPosition.y, targetPosition.y, t) + parabolaHeight * (1f - Mathf.Pow((newX - midX) / (targetPosition.x - midX), 2f));

                    transform.position = new Vector3(newX, newY, startPosition.z);
                }
            }

            Tween MoveDown(Transform transform, float doodlerHeight, float duration)
            {
                var screenRect = _cameraService.GetScreenRect();
                var screenHeightCount = 1.5f;

                return transform.DOMoveY(transform.position.y - screenRect.height * screenHeightCount - doodlerHeight, duration);
            }
        }

        public void End()
        {
            _rigidbody.simulated = true;
            _sequence?.Kill();
        }
    }
}
