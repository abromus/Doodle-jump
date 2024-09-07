using System;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Platforms
{
    internal sealed class TemporaryPlatform : Platform, IUpdatable
    {
        private bool _initialized;
        private bool _triggered;
        private IUpdater _updater;
        private TemporaryPlatformConfig _config;
        private TemporaryPlatformCollisionInfo _info;
        private float _existenceTime;

        public override event Action<IPlatformCollisionInfo> Collided;

        public override event Action<IPlatform> Destroyed;

        public override void Init(IGameData gameData)
        {
            base.Init(gameData);

            _updater = gameData.CoreData.ServiceStorage.GetUpdater();
        }

        public override void InitConfig(IPlatformConfig platformConfig)
        {
            _config = (TemporaryPlatformConfig)platformConfig;
            _info = new TemporaryPlatformCollisionInfo(this, _config.ExistenceTime);
        }

        public override void InitPosition(Vector3 position)
        {
            base.InitPosition(position);

            ResetExistenceTime();

            _triggered = false;
            _initialized = true;

            Subscribe();
        }

        public void Tick(float deltaTime)
        {
            if (_initialized == false || _triggered == false)
                return;

            _existenceTime -= deltaTime;

            if (0f < _existenceTime)
                return;

            Unsubscribe();
            Destroy();
        }

        public override void Destroy()
        {
            Destroyed.SafeInvoke(this);
        }

        private void OnEnable()
        {
            if (_initialized)
                Subscribe();
        }

        private void OnDisable()
        {
            if (_initialized)
                Unsubscribe();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (0f < collision.relativeVelocity.y || collision.transform.TryGetComponent<IDoodler>(out var doodler) == false)
                return;

            _triggered = true;

            _info.UpdateTime(_existenceTime);

            PlaySound(СlipType);

            Collided.SafeInvoke(_info);
        }

        private void ResetExistenceTime()
        {
            _existenceTime = _config.ExistenceTime;

            _info.UpdateTime(_existenceTime);
        }

        private void Subscribe()
        {
            _updater.AddUpdatable(this);
        }

        private void Unsubscribe()
        {
            _updater.RemoveUpdatable(this);
        }
    }
}
