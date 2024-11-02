using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal readonly struct CameraFollowerArgs
    {
        private readonly ICameraService _cameraService;
        private readonly Transform _worldTransform;
        private readonly Transform _doodlerTransform;
        private readonly Transform _cameraTransform;
        private readonly Vector3 _cameraOffset;
        private readonly float _animationDelay;
        private readonly float _animationDuration;

        internal readonly ICameraService CameraService => _cameraService;

        internal readonly Transform WorldTransform => _worldTransform;

        internal readonly Transform DoodlerTransform => _doodlerTransform;

        internal readonly Transform CameraTransform => _cameraTransform;

        internal readonly Vector3 CameraOffset => _cameraOffset;

        internal readonly float AnimationDelay => _animationDelay;

        internal readonly float AnimationDuration => _animationDuration;

        internal CameraFollowerArgs(
            ICameraService cameraService,
            Transform worldTransform,
            Transform doodlerTransform,
            Transform cameraTransform,
            in Vector3 cameraOffset,
            float animationDelay,
            float animationDuration)
        {
            _cameraService = cameraService;
            _worldTransform = worldTransform;
            _doodlerTransform = doodlerTransform;
            _cameraTransform = cameraTransform;
            _cameraOffset = cameraOffset;
            _animationDelay = animationDelay;
            _animationDuration = animationDuration;
        }
    }
}
