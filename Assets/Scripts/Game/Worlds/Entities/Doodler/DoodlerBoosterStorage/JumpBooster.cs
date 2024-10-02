using DoodleJump.Core;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class JumpBooster : Boosters.BaseBooster, Core.Services.IUpdatable, Core.Services.IPausable
    {
        private Settings.IJumpBoosterConfig _config;
        private Core.Services.IUpdater _updater;
        private IDoodler _doodler;
        private UnityEngine.Rigidbody2D _rigidbody;

        private bool _isPaused;
        private float _currentTime;
        private float _jumpForceFactor;

        public override event System.Action<Boosters.IBooster> Executed;

        public override void Init(Settings.IBoosterConfig boosterConfig, in DoodlerBoosterStorageArgs args)
        {
            _config = (Settings.IJumpBoosterConfig)boosterConfig;
            _updater = args.Updater;
            _doodler = args.Doodler;
            _rigidbody = args.Rigidbody;
        }

        public override void Execute()
        {
            _currentTime = _config.ExistenseTime;
            _jumpForceFactor = _config.JumpForceFactor;

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

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Show()
        {
            SpriteRenderer.enabled = true;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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

            _doodler.Jumped += OnJumped;
        }

        private void Unsubscribe()
        {
            _updater.RemoveUpdatable(this);
            _updater.RemovePausable(this);

            _doodler.Jumped -= OnJumped;
        }

        private void OnJumped()
        {
            var velocity = _rigidbody.velocity;
            velocity.y *= _jumpForceFactor;
            _rigidbody.velocity = velocity;
        }
    }
}
