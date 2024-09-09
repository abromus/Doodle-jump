namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IProjectile
    {
        public UnityEngine.GameObject GameObject { get; }

        public event System.Action<IProjectile> Destroyed;

        public void Init(Services.IAudioService audioService, Core.Services.IUpdater updater, Core.Services.ICameraService cameraService);

        public void InitPosition(UnityEngine.Vector3 position, float doodlerDirection, UnityEngine.Vector3 shootPosition);

        public void Clear();
    }
}
