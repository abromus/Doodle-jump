namespace DoodleJump.Game
{
    internal sealed class GameSceneController : Core.BaseSceneController
    {
        [UnityEngine.SerializeField] private Settings.ConfigStorage _configStorage;
        [UnityEngine.SerializeField] private UnityEngine.Transform _uiServicesContainer;

        private IGame _game;

        public override void Run(Core.Data.ICoreData coreData)
        {
            _game = new Game(coreData, _configStorage, _uiServicesContainer);
            _game.Run();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Destroy()
        {
            _game.Destroy();
        }

        private void Awake()
        {
            _configStorage.Init();
        }

        private void OnDestroy()
        {
            Destroy();
        }
    }
}
