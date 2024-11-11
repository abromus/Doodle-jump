namespace DoodleJump.Game.Worlds.Entities
{
    internal readonly struct DoodlerAnimatorArgs
    {
        private readonly UnityEngine.Transform _doodlerTransform;
        private readonly UnityEngine.Animator _doodlerAnimator;
        private readonly UnityEngine.GameObject _head;
        private readonly UnityEngine.GameObject _shootingHead;
        private readonly IDoodlerInput _doodlerInput;
        private readonly IDoodlerMovement _doodlerMovement;
        private readonly Data.IPlayerData _playerData;
        private readonly Settings.IDoodlerConfig _doodlerConfig;

        internal readonly UnityEngine.Transform DoodlerTransform => _doodlerTransform;

        internal readonly UnityEngine.Animator DoodlerAnimator => _doodlerAnimator;

        internal readonly UnityEngine.GameObject Head => _head;

        internal readonly UnityEngine.GameObject ShootingHead => _shootingHead;

        internal readonly IDoodlerInput DoodlerInput => _doodlerInput;

        internal readonly IDoodlerMovement DoodlerMovement => _doodlerMovement;

        internal readonly Data.IPlayerData PlayerData => _playerData;

        internal readonly Settings.IDoodlerConfig DoodlerConfig => _doodlerConfig;

        internal DoodlerAnimatorArgs(
            UnityEngine.Transform doodlerTransform,
            UnityEngine.Animator doodlerAnimator,
            UnityEngine.GameObject head,
            UnityEngine.GameObject shootingHead,
            IDoodlerInput doodlerInput,
            IDoodlerMovement doodlerMovement,
            Data.IPlayerData playerData,
            Settings.IDoodlerConfig doodlerConfig)
        {
            _doodlerTransform = doodlerTransform;
            _doodlerAnimator = doodlerAnimator;
            _head = head;
            _shootingHead = shootingHead;
            _doodlerInput = doodlerInput;
            _doodlerMovement = doodlerMovement;
            _playerData = playerData;
            _doodlerConfig = doodlerConfig;
        }
    }
}
