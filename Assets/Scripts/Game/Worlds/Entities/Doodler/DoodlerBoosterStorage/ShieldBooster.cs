﻿using DoodleJump.Core;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class ShieldBooster : Boosters.Booster, Core.Services.IUpdatable, Core.Services.IPausable
    {
        private Settings.IShieldBoosterConfig _config;
        private Core.Services.IUpdater _updater;

        private bool _isPaused;
        private float _currentTime;

        public override event System.Action<Boosters.IBooster> Executed;

        public override void Init(Settings.IBoosterConfig boosterConfig, in DoodlerBoosterStorageArgs args)
        {
            _config = (Settings.IShieldBoosterConfig)boosterConfig;
            _updater = args.Updater;
        }

        public override void Execute()
        {
            _currentTime = _config.ExistenseTime;

            transform.localPosition = UnityEngine.Vector3.zero;

            Show();
            Subscribe();
        }

        public override void Destroy()
        {
            Unsubscribe();
            Hide();
        }

        public void Tick(float deltaTime)
        {
            UpdateTime(deltaTime);
            CheckTime();
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        private void Show()
        {
            SpriteRenderer.enabled = true;
        }

        private void Hide()
        {
            SpriteRenderer.enabled = false;
        }

        private void UpdateTime(float deltaTime)
        {
            if (_isPaused == false)
                _currentTime -= deltaTime;
        }

        private void CheckTime()
        {
            if (0f < _currentTime)
                return;

            _currentTime = 0f;

            Executed.SafeInvoke(this);
        }

        private void Subscribe()
        {
            _updater.AddUpdatable(this);
            _updater.AddPausable(this);
        }

        private void Unsubscribe()
        {
            _updater.RemoveUpdatable(this);
            _updater.RemovePausable(this);
        }
    }
}