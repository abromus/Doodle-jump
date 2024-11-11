namespace DoodleJump.Game.Worlds.Entities
{
    internal readonly struct DoodlerHeadAnimatorArgs
    {
        private readonly UnityEngine.GameObject _head;
        private readonly UnityEngine.GameObject _shootingHead;
        private readonly IDoodlerInput _doodlerInput;
        private readonly Data.IPlayerData _playerData;
        private readonly float _shootModeDuration;

        internal readonly UnityEngine.GameObject Head => _head;

        internal readonly UnityEngine.GameObject ShootingHead => _shootingHead;

        internal readonly IDoodlerInput DoodlerInput => _doodlerInput;

        internal readonly Data.IPlayerData PlayerData => _playerData;

        internal readonly float ShootModeDuration => _shootModeDuration;

        internal DoodlerHeadAnimatorArgs(
            UnityEngine.GameObject head,
            UnityEngine.GameObject shootingHead,
            IDoodlerInput doodlerInput,
            Data.IPlayerData playerData,
            float shootModeDuration)
        {
            _head = head;
            _shootingHead = shootingHead;
            _doodlerInput = doodlerInput;
            _playerData = playerData;
            _shootModeDuration = shootModeDuration;
        }
    }
}
