namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerHeadAnimator : IDoodlerHeadAnimator
    {
        private bool _isPaused;
        private bool _isShootMode;
        private float _shootModeElapsed;

        private readonly UnityEngine.GameObject _head;
        private readonly UnityEngine.GameObject _shootingHead;
        private readonly IDoodlerInput _doodlerInput;
        private readonly Data.IPlayerData _playerData;
        private readonly float _shootModeDuration;

        internal DoodlerHeadAnimator(in DoodlerHeadAnimatorArgs args)
        {
            _head = args.Head;
            _shootingHead = args.ShootingHead;
            _doodlerInput = args.DoodlerInput;
            _playerData = args.PlayerData;
            _shootModeDuration = args.ShootModeDuration;
        }

        public void Restart()
        {
            ResetShootMode();
            ActivateShootHead(false);
        }

        public void Tick(float deltaTime)
        {
            if (_isPaused)
                return;

            if (CanShoot())
            {
                ResetShootMode();
                ActivateShootHead(true);

                _isShootMode = true;
            }
            else if (_isShootMode)
            {
                _shootModeElapsed += deltaTime;

                if (_shootModeDuration < _shootModeElapsed)
                {
                    ResetShootMode();
                    ActivateShootHead(false);
                }
            }
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private bool CanShoot()
        {
            return _doodlerInput.IsShooting && _isPaused == false && 0 < _playerData.CurrentShots;
        }

        private void ResetShootMode()
        {
            _isShootMode = false;
            _shootModeElapsed = 0f;
        }

        private void ActivateShootHead(bool isActive)
        {
            _head.SetActive(isActive == false);
            _shootingHead.SetActive(isActive);
        }
    }
}
