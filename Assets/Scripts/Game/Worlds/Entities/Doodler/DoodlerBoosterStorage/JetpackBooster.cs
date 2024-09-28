using DoodleJump.Core;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class JetpackBooster : Boosters.Booster, Core.Services.IUpdatable, Core.Services.IPausable
    {
        private Settings.IJetpackBoosterConfig _config;
        private Core.Services.IUpdater _updater;
        private UnityEngine.Rigidbody2D _rigidbody;

        private bool _isPaused;
        private float _currentTime;
        private UnityEngine.Vector2 _defaultVelocity;
        private UnityEngine.Vector2 _currentVelocity;

        public override event System.Action<Boosters.IBooster> Executed;

        public override void Init(Settings.IBoosterConfig boosterConfig, in DoodlerBoosterStorageArgs args)
        {
            _config = (Settings.IJetpackBoosterConfig)boosterConfig;
            _updater = args.Updater;
            _rigidbody = args.Rigidbody;
        }

        public override void Execute()
        {
            _currentTime = _config.ExistenseTime;
            _defaultVelocity = _rigidbody.velocity;

            var velocity = _rigidbody.velocity;
            velocity.x = 0f;
            velocity.y += _config.JumpForce;
            _currentVelocity = velocity;
            _rigidbody.velocity = velocity;
            _rigidbody.isKinematic = true;

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
            CheckVelocity();
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

        private void CheckVelocity()
        {
            _rigidbody.velocity = 0f < _currentTime ? _currentVelocity : _defaultVelocity;

            if (_currentTime <= 0f)
                _rigidbody.isKinematic = false;
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
